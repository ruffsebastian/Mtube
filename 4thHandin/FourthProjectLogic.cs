using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static void SearchMovies(string searchterm) { HttpContext.Current.Response.Redirect("~/search/?queryName=" + searchterm); }

        public static string GetJokeFromAPI()
        {
            WebClient client = new WebClient();

            string[] Teachers = { "Torben", "Tue", "Morten", "Jesper" };
            string reply = client.DownloadString("http://api.icndb.com/jokes/random?firstName=" + Teachers[new Random().Next(0, Teachers.Length)]);

            string[] separatingChars = { "\"" };
            string[] mysplit = reply.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            if (mysplit[0] != "False"){ return mysplit[11];}
                                 else { return "i dont get it"; };
        }

        public class OmdbAPI
        {
            private static WebClient client = new WebClient();
            private static string OmdbApiToken = "a4036190";

            public static string NameAPI(string name,int year)
            {
                string result = client.DownloadString("http://www.omdbapi.com/?apikey=" + OmdbApiToken + "&r=xml&t=" + name+"&y="+year);
                return result;
            }

            public static string GetPosterUrl(string title,int year)
            {
                string result = OmdbAPI.NameAPI(title,year);
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

        public class Commercials
        {
            // https://www.freeformatter.com/xsl-transformer.html Nice xslt testing       

            // make sure we have the transformed xml, if not then create it
            public static void CheckTransform()
            {
                // do the xslt transformation on the commercials.xml only if we havent done so, or if we deleted it to force a refresh
                if (!File.Exists(HttpContext.Current.Server.MapPath("/xml/commercialsTransformed.xml")))  //might wanna expand this to check if we have rows in commercialsTransformed to make sure we have GOOD xml, not just files.
                {
                    string sourcefile = HttpContext.Current.Server.MapPath("/xml/commercials.xml");
                    string xslfile = HttpContext.Current.Server.MapPath("/xml/commercialsImport.xslt");

                    string destinationfile = HttpContext.Current.Server.MapPath("/xml/commercialsTransformed.xml");

                    FileStream fs = new FileStream(destinationfile, FileMode.Create);
                    XslCompiledTransform xct = new XslCompiledTransform();
                    xct.Load(xslfile);

                    xct.Transform(sourcefile, null, fs);
                    fs.Close();
                }
            }

            //needed for saving since we cant directly do that
            public static void MakeTempXml()
            {
                string sourcefile = HttpContext.Current.Server.MapPath("/xml/commercialsTransformed.xml");
                string xslfile = HttpContext.Current.Server.MapPath("/xml/commercialsCopy.xslt");

                string destinationfile = HttpContext.Current.Server.MapPath("/xml/commercialsTransformedTemp.xml");

                FileStream fs = new FileStream(destinationfile, FileMode.Create);
                XslCompiledTransform xct = new XslCompiledTransform();
                xct.Load(xslfile);
                xct.Transform(sourcefile, null, fs);
                fs.Close();
            }

            //viewcounts for commercials
            public static int StatTracker()
            {
                CheckTransform();

                int randomcommercialToDisplayPosition = new Random().Next(0, 4); //should be a count, otherwise new ones won't be seen

                XsltArgumentList argsList = new XsltArgumentList();
                argsList.AddParam("randomcommercialToDisplayPosition", "", randomcommercialToDisplayPosition);

                string sourcefile = HttpContext.Current.Server.MapPath("/xml/commercialsTransformedTemp.xml");
                string xslfile = HttpContext.Current.Server.MapPath("/xml/commercialsIncrementer.xslt");
                string destinationfile = HttpContext.Current.Server.MapPath("/xml/commercialsTransformed.xml");

                FileStream fs = new FileStream(destinationfile, FileMode.Create);
                XslCompiledTransform xct = new XslCompiledTransform();
                xct.Load(xslfile);
                xct.Transform(sourcefile, argsList, fs);
                fs.Close();

                MakeTempXml();  //keep the temp file updated... updatetemp would probably be a better name 

                return randomcommercialToDisplayPosition;
            }
        }

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
                this.genre = genre;
                this.year = year;
                this.viewcount = viewcount;
                this.posterpath = posterpath;
            }
            
            public Movie(int ID) //constructor via DataSet - essentially the same as searching for movie by id but more sexier
            {
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByID(ID);

                this.id = Int32.Parse(movieDBListRows[0]["id"].ToString());
                this.title = movieDBListRows[0]["title"].ToString();
                this.genre = movieDBListRows[0]["genre"].ToString();
                this.year = Int32.Parse(movieDBListRows[0]["year"].ToString());
                this.viewcount = Int32.Parse(movieDBListRows[0]["viewcount"].ToString());
                this.posterpath = movieDBListRows[0]["posterpath"].ToString();
            }

            public override string ToString()
            {
                string outputtet = "That Movie " + this.title + ", i think it was made in " + this.year + " or so... was one of those " + this.genre;
                outputtet += " flicks... folks round here have taken a shine to it " + this.viewcount + " times. you can find its poster at ye olde uniform resource locator " + this.posterpath;
                return outputtet;
            }

            public void IncrementViewcount()
            {
                MovieTableAdapter.Update( this.title, this.genre, this.year, this.viewcount + 1, this.posterpath, this.id, this.id);
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
                DataAccessLayer.MovieDBListDataTable movieDBListRows = MovieTableAdapter.GetDataByGenre(Genre);  
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