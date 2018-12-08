using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4thHandin
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Display a Chuck Norris joke via API about a random teacher
           LabelJoke.Text = FourthProjectLogic.GetJokeFromAPI();


           


            
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            FourthProjectLogic.SearchMovies(TextBoxSearch.Text);
        }
    }
}