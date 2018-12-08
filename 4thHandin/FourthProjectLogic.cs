using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace _4thHandin
{
    class FourthProjectLogic 
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionString"].ToString();

        public static string GetJokeFromAPI()
        {
            WebClient client = new WebClient();

            string[] Teachers = { "Torben", "Tue", "Morten", "Jesper" };
            string reply = client.DownloadString("http://api.icndb.com/jokes/random?firstName=" + Teachers[new Random().Next(0, Teachers.Length)]);

            string[] separatingChars = { "\"" };
            string[] mysplit = reply.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            if (mysplit[0] != "False")
            {
                return mysplit[11]; //i feel a bit dirty but hey, results are results right? [no they are not]
            }
            else {
                return "i dont get it";
            };
        }

        public class OmdbAPI
        {
            private static WebClient client = new WebClient();
            private static string OmdbApiToken = "a4036190";

            public static string NameAPI(string name)
            {

                string result = client.DownloadString("http://www.omdbapi.com/?apikey=" + OmdbApiToken + "&r=xml&t=" + name);
                return result;

            }

            internal static string NameAPI(int v)
            {
                throw new NotImplementedException();
            }

            public static string GetPosterUrl(string title)
            {
                string result = OmdbAPI.NameAPI(title);
                string poster = "failed to get poster";

                File.WriteAllText(HttpContext.Current.Server.MapPath("~/MyFiles/Latestresult.xml"), result);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);

                if (doc.SelectSingleNode("/root/@response").InnerText == "True")
                {
                    XmlNodeList nodelist = doc.SelectNodes("/root/movie");
                    foreach (XmlNode node in nodelist)
                    {
                        poster = node.SelectSingleNode("@poster").InnerText;
                    }

                }
                return poster;
            }

        }

        public static void SearchMovies(string searchterm) {
                HttpContext.Current.Response.Redirect("~/search/?queryName=" + searchterm);
        }

        public static void CheckCommercials()
        {
            // do the xslt transformation on the commercials.xml only if we havent done so, or if we deleted it to force a refresh
            if (!File.Exists(HttpContext.Current.Server.MapPath("xml/commercialsTransformedAttr.xml")))
            {
                string sourcefile = HttpContext.Current.Server.MapPath("xml/commercials.xml");
                string xslfile = HttpContext.Current.Server.MapPath("xml/commercialsXSLT - Attributes.xslt");

                string destinationfile = HttpContext.Current.Server.MapPath("xml/commercialsTransformedAttr.xml");

                FileStream fs = new FileStream(destinationfile, FileMode.Create);
                XslCompiledTransform xct = new XslCompiledTransform();
                xct.Load(xslfile);
                xct.Transform(sourcefile, null, fs);
                fs.Close();
            }
        }

        /* xsltargumetnlist example, for viewcount */
        /*
        public static void TrackCommercialViews(int randomcommercialToDisplayPosition)
        {
            XsltArgumentList argsList = new XsltArgumentList();
            argsList.AddParam("position", "", randomcommercialToDisplayPosition);

            string sourcefile = HttpContext.Current.Server.MapPath("xml/commercialsTransformedAttr.xml");
            string xslfile = HttpContext.Current.Server.MapPath("xml/commercialsXSLT - Increment.xslt");

            string destinationfile = HttpContext.Current.Server.MapPath("xml/commercialsTransformedAttr.xml");

            FileStream fs = new FileStream(destinationfile, FileMode.Create);
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(xslfile);
            xct.Transform(sourcefile, argsList, fs);
            fs.Close();
        }
        */

        public class Movie  
        {
            public static DataAccessLayerTableAdapters.MovieDBListTableAdapter MovieTableAdapter = new DataAccessLayerTableAdapters.MovieDBListTableAdapter();
            
            public int id;
            public string title;
            public string genre;
            public int year;
            public int viewcount;
            public string posterpath;
            public Movie(int id, string title, string genre, int year, int viewcount, string posterpath)    //constructor setting all properties from passed values
            {
                this.id = id;
                this.title = title;
                this.genre = title;
                this.year = year;
                this.viewcount = viewcount;
                this.posterpath = posterpath;
            }
            //constructor via DataSet - essentially the same as searching for movie by id but more efficient code-wise... 
            //might be a bit cheeky tho, certainly dont wanna mass-create objects like this(since it would probably be calling the db for every object)
            //its a bit annoying having to instantiate a movie to get the value too kinda... although having the object is very handy
            //usage example:  FourthProjectLogic.Movie themovie = new FourthProjectLogic.Movie(queryID);  and then something like textbox.text = themovie.title;
            public Movie (int ID)                                     
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByID(ID);

                this.id = Int32.Parse(movieDBListRows[0]["id"].ToString());
                this.title = movieDBListRows[0]["title"].ToString();
                this.genre = movieDBListRows[0]["genre"].ToString();
                this.year = Int32.Parse(movieDBListRows[0]["year"].ToString());
                this.viewcount = Int32.Parse(movieDBListRows[0]["viewcount"].ToString());
                this.posterpath = movieDBListRows[0]["posterpath"].ToString();
            }

            //early test of MovieDBListDataTable capabilities, only gets the title instead of creating an object.
            //usage example:  textbox.text = FourthProjectLogic.GetTitleByIdDal(queryID);
            public static String GetTitleByIdDal(int ID)
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByID(ID);

                return movieDBListRows[0][1].ToString();
            }

            public override string ToString()
            {
                string outputtet = "That Movie " + this.title + ", i think it was made in " + this.year + " or so... was one of those " + this.genre;
                       outputtet += " flicks... folks round here have taken a shine to it " + this.viewcount + " times. you can find its poster at ye olde uniform resource locator " + this.posterpath;
                return outputtet;
            }            

            public void IncrementViewcount()
            {
                MovieTableAdapter.Update(this.title, this.genre, this.year, this.viewcount + 1, this.posterpath, this.id, this.id);
            }

            public static List<Movie> ListMoviesByGenre(string Genre)  
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByGenre(Genre);   // this shit right here... 
                return MovieListLoader(movieDBListRows);
            }

            public static List<Movie> MovieListLoader(DataAccessLayer.MovieDBListDataTable movieDBListRows)
            {
                var movieList = new List<Movie>();

                foreach (DataAccessLayer.MovieDBListRow row in movieDBListRows)
                {
                    int id = Int32.Parse(row["id"].ToString());
                    string title = row["title"].ToString();
                    string genre = row["genre"].ToString();
                    int year = Int32.Parse(row["year"].ToString());
                    int viewcount = Int32.Parse(row["viewcount"].ToString());
                    string posterpath = row["posterpath"].ToString();
                    Movie readmovie = new Movie(id, title, genre, year, viewcount, posterpath);
                    movieList.Add(readmovie);
                }
                return movieList;
            }

            // fuck everything below here

            /* below method is a bit of a marrige of new and old - at the moment im only using the byid case and only want one result. should make a replacement that uses stored procedures like IncrementViewcount  */
            /* Point being: THIS METHODS CODE IS PRETTY STUPID, DONT IMITATE */
            public static List<Movie> ListMovies(string how, string what)
            {
                string mycommandtext = "";
                switch (how)
                {
                    case "byid":
                        mycommandtext = "SELECT * FROM MovieDBList WHERE id = '" + what + "'";  //make distinct title + year
                        break;
                    case "category":
                        mycommandtext = "SELECT * FROM MovieDBList WHERE category = '" + what + "'";
                        break;
                    case "year": //why not?
                        mycommandtext = "SELECT * FROM MovieDBList WHERE year = '" + what + "'";  //make distinct title + year
                        break;
                    case "top10":
                        mycommandtext = "SELECT TOP (10) * FROM MovieDBList ORDER BY ViewCount DESC, Year DESC";  //make distinct title + year
                        break;
                }
                var movieList = new List<Movie>();
                using (SqlConnection cn = new SqlConnection
                { ConnectionString = FourthProjectLogic.ConnStr })
                {
                    using (SqlCommand cmd = new SqlCommand
                    { Connection = cn, CommandText = mycommandtext })
                    {
                        cn.Open();
                        var Reader = cmd.ExecuteReader();
                        if (Reader.HasRows)
                        {
                            while (Reader.Read())              //This part is solid enough though, here is where we load our results from the db reader into Movie objects and add each of them to the list of movies from line 78
                            {
                                int id = Reader.GetFieldValue<int>(0);
                                string title = Reader.GetString(Reader.GetOrdinal("title"));
                                string genre = Reader.GetString(Reader.GetOrdinal("genre"));
                                int year = Reader.GetFieldValue<int>(3); //Reader.GetOrdinal("year").ToString();
                                int viewcount = Reader.GetFieldValue<int>(4); //Reader.GetOrdinal("viewcount").ToString();
                                string posterpath = Reader.GetString(Reader.GetOrdinal("posterpath"));
                                Movie readmovie = new Movie(id, title, genre, year, viewcount, posterpath);  //for some reason i found it simpler to just deal with the ints like this, reader.get* got me confused.... might just be brainfarting---- probably certainly
                                movieList.Add(readmovie); //readmovie as in the movie that was read, not an order... hm, these comments might need some coffe.
                            }
                        }
                        Reader.Close();
                    }
                }
                return movieList;
            }


            public static List<Movie> ListMoviesCateogry(string genre)
            {

                /* wait why am i opening connections, cant the dataset/dataaccesslayer.xsd/tableadapters do this for me?

                NorthwindDataSetTableAdapters.RegionTableAdapter regionTableAdapter =
    new NorthwindDataSetTableAdapters.RegionTableAdapter();

                regionTableAdapter.Insert(5, "NorthWestern");
                https://docs.microsoft.com/en-us/visualstudio/data-tools/directly-access-the-database-with-a-tableadapter?view=vs-2017
                https://docs.microsoft.com/en-us/visualstudio/data-tools/fill-datasets-by-using-tableadapters?view=vs-2017
                 */



                DataAccessLayer.MovieDBListDataTable dt = new DataAccessLayer.MovieDBListDataTable();
                
                DataAccessLayerTableAdapters.MovieDBListTableAdapter tableAdapter = new DataAccessLayerTableAdapters.MovieDBListTableAdapter();

                //dt.GetObjectData.
               // dt. tableAdapter.GetDataByGenre(genre);
                
                //okay im too tired to make it work with a query but apparently we can go directly to the data like this, fuck me XD

                var movieList = new List<Movie>();

                return movieList;
         /*       SqlConnection con = new SqlConnection(FourthProjectLogic.ConnStr);
                SqlCommand cmd = new SqlCommand("", con) { CommandType = CommandType.StoredProcedure };  //by id from param from object 
                cmd.Parameters.AddWithValue("@Genre", this.genre);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();*/
            }

        }
     
    }
}