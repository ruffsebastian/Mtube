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

        public class Movie  
        {
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
            public Movie( string id)             //constructor setting properties by getting a list of movies by id (because we dont have a method for getting a single movie at the moment, laziness i guess)  and grabbing values from the only result
            {
                var movieList = new List<Movie>(ListMovies("byid", id));   //360 noscope shit right here - create the list of movies and fill it from method
                
                // [below] very optimistic guess at property access? eh, would work in js same for the rest of em .... ugh, what the fuck is it i need to do.. .list of items, not arraylist JUST DO IT <<---- This was a really good idea
                // context: arraylists suck balls.
                this.id = movieList[0].id; 
                this.title = movieList[0].title;
                this.genre = movieList[0].title;
                this.year = movieList[0].year;
                this.viewcount = movieList[0].viewcount;
                this.posterpath = movieList[0].posterpath;

                //fill with default values, leave off, get from db via id? trying via db   <-----That worked, wow.
                //not sure if smart or retarded - could i be getting this info easier? probably, but this is pretty clever anyway.
            }

            public override string ToString()
            {
                string outputtet = "This Movie is called " + this.title + ", was made in " + this.year + " and is in the genre " + this.genre;
                       outputtet += "it has been viewed " + this.viewcount + " times. its poster url is " + this.posterpath;
                return outputtet;
            }

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

            /* usage guesstimation: incviewcount when details viewed - called on singleview pageload 

            Movie mToUpdate = new Movie(query) // make a constructor that takes one arg (blindcoded, see if it works)
            mToUpdate.IncrementViewcount();

                sweet, it worked - just changed the names of the var and query - also, very useful to have in general so named it themovie instead of keeping it named to this scope of usage.

            */

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

            public static DataAccessLayerTableAdapters.MovieDBListTableAdapter MovieTableAdapter = new DataAccessLayerTableAdapters.MovieDBListTableAdapter();

            /* now we're talking */
            public void IncrementViewcount2()
            {
                MovieTableAdapter.Update(this.title, this.genre, this.year, this.viewcount + 1, this.posterpath, this.id, this.id);
            }


            public static String GetTitleByIdDal(int ID)
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByID(ID);

                return movieDBListRows[0][1].ToString();
            }

            public static Movie GetById(int ID)
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByID(ID);
                
                int id = Int32.Parse(movieDBListRows[0]["id"].ToString());
                string title = movieDBListRows[0]["title"].ToString();
                string genre = movieDBListRows[0]["genre"].ToString();
                int year = Int32.Parse(movieDBListRows[0]["year"].ToString());
                int viewcount = Int32.Parse(movieDBListRows[0]["viewcount"].ToString());
                string posterpath = movieDBListRows[0]["posterpath"].ToString();

                Movie movie = new Movie(id, title, genre, year, viewcount, posterpath);

                return movie;
            }


            public static Movie GetByIdOld(int ID)
            {
                var movieList = new List<Movie>();

                DataTable dt = MovieTableAdapter.GetDataByID(ID);
                    DataTableReader dtr = new DataTableReader(dt);                
                while (dtr.Read()) { 
                    foreach (DataRow row in dtr)
                    {
                        

                        Movie readmovie = new Movie(id, title, genre, year, viewcount, posterpath);
                        movieList.Add(readmovie);
                    }
                }
                return movieList[0]; 
            }


            //non-query stored procedure example via modified movie object - uses the automatically constructed stored procedure for updating movies 
            public void IncrementViewcount()
            {
                this.viewcount = this.viewcount + 1; //increment the value of the object before updating it in the db with the new values

                SqlConnection con = new SqlConnection(FourthProjectLogic.ConnStr);
                SqlCommand cmd = new SqlCommand("MovieUpdateCommand", con){CommandType = CommandType.StoredProcedure};  //by id from param from object 
                cmd.Parameters.AddWithValue("@Id", this.id);
                cmd.Parameters.AddWithValue("@Original_Id", this.id);
                cmd.Parameters.AddWithValue("@Title", this.title);
                cmd.Parameters.AddWithValue("@Genre", this.genre);
                cmd.Parameters.AddWithValue("@Year", this.year);
                cmd.Parameters.AddWithValue("@Viewcount", this.viewcount);
                cmd.Parameters.AddWithValue("@Posterpath", this.posterpath);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
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
    }
}