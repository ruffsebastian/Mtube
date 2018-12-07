using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _4thHandin
{
    class ProjectLogic
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionString"].ToString();

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
                var movieList = new List<Movie>(ListMovies("byid", id));   //360 noscope shit right here
                
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
                { ConnectionString = ProjectLogic.ConnStr })
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

            //non-query stored procedure example via modified movie object - uses the automatically constructed stored procedure for updating movies 
            public void IncrementViewcount()
            {
                this.viewcount = this.viewcount + 1;

                SqlConnection conn = new SqlConnection(ProjectLogic.ConnStr);
                SqlCommand cmd = new SqlCommand("MovieUpdateCommand", conn);  //by id from param from object 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.id);
                cmd.Parameters.AddWithValue("@Original_Id", this.id);
                cmd.Parameters.AddWithValue("@Title", this.title);
                cmd.Parameters.AddWithValue("@Genre", this.genre);
                cmd.Parameters.AddWithValue("@Year", this.year);
                cmd.Parameters.AddWithValue("@Viewcount", this.viewcount);
                cmd.Parameters.AddWithValue("@Posterpath", this.posterpath);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}