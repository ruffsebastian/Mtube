using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4thHandin
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //examples of getting movie info from database 

            //creating a movie object using the ID constructor - this finds the movie with this id in the database and loads it into the movie object it creates 
            FourthProjectLogic.Movie testmovie = new FourthProjectLogic.Movie(420);
            //doyouhave2012.Text = testmovie.ToString();  //we then use our movie objects custom tostring method to output some summary text about the movie (that i wrote way too late at night)

            //stuck me that we could also do this to get that info:
            //doyouhave2012.Text = FourthProjectLogic.Movie.MovieTableAdapter.GetDataByID(420).ToString();

            //using one of the movie objects list methods to find all movies with the genre thriller and count the rows.
            //doyouhavefrozen.Text = "there are " + FourthProjectLogic.Movie.ListMoviesByGenre("Thriller").Count + " movies in the genre Thriller";

                        //we could also do this to get the first thriller movie in our list, lol.
            //doyouhave2012.Text = FourthProjectLogic.Movie.ListMoviesByGenre("Thriller")[0].ToString();
           
            // END OF EXAMPLES

            // TOP 10
            //set datasource - alternative to designer setup - get top 10
            //Repeater1.DataSource = FourthProjectLogic.Movie.MovieTableAdapter.MoviesTop10();
            //Repeater1.DataBind();



        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
           FourthProjectLogic.SearchMovies(TextBox1.Text);
        }
    }
}