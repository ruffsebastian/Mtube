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
            doyouhave2012.Text = FourthProjectLogic.Movie.GetTitleByIdDal(420);

            FourthProjectLogic.Movie testmovie = new FourthProjectLogic.Movie(420);

            doyouhavefrozen.Text = "there are " + FourthProjectLogic.Movie.ListMoviesByGenre("Thriller").Count + " movies in the genre Thriller";
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
           FourthProjectLogic.SearchMovies(TextBoxSearch.Text);
        }
    }
}