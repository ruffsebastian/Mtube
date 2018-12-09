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

            private static List<Movie> MovieListLoader(DataAccessLayer.MovieDBListDataTable movieDBListRows)
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

            public static List<Movie> ListMoviesByGenre(string Genre)  
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByGenre(Genre);   // this shit right here... 
                return MovieListLoader(movieDBListRows);
            }                        

            public static List<Movie> ListMoviesByTitle(string Title)
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByTitle(Title);
                return MovieListLoader(movieDBListRows);
            }

            public static List<Movie> ListMoviesTop10()
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.MoviesTop10();
                return MovieListLoader(movieDBListRows);
            }


        }
     
    }
}