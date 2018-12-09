using System;
using System.Collections.Generic;
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
            FourthProjectLogic.Movie testmovie = new FourthProjectLogic.Movie(420);
            doyouhave2012.Text = testmovie.ToString();
            doyouhavefrozen.Text = "there are " + FourthProjectLogic.Movie.ListMoviesByGenre("Thriller").Count + " movies in the genre Thriller";

            //set datasource - alternative to designer setup
            Repeater1.DataSource = FourthProjectLogic.Movie.MovieTableAdapter.MoviesTop10();
            Repeater1.DataBind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
           FourthProjectLogic.SearchMovies(TextBoxSearch.Text);
        }
    }
}