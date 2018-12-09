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
            FourthProjectLogic.Movie testmovie = new FourthProjectLogic.Movie(420);
            doyouhave2012.Text = testmovie.ToString();
            doyouhavefrozen.Text = "there are " + FourthProjectLogic.Movie.ListMoviesByGenre("Thriller").Count + " movies in the genre Thriller";

            //set datasource - alternative to designer setup
            Repeater1.DataSource = FourthProjectLogic.Movie.MovieTableAdapter.MoviesTop10();
            Repeater1.DataBind();



                // make sure we have the transformed xml, if not create it
                FourthProjectLogic.CheckCommercials();

                

            // run the logic for commercial stat tracking - reading and modifying the xml to increment viewcount for the random commercial and passing the commercials id/rowindex/"position"
            int randomcommercialToDisplayPosition = FourthProjectLogic.CommercialStatTracker();

            doyouhave2012.Text = randomcommercialToDisplayPosition.ToString();


            // load the xml for display 
            DataSet xmldata = new DataSet();
                xmldata.ReadXml(MapPath("~/xml/commercialsTransformed.xml"));
                GridViewCommercial.DataSource = xmldata;
                GridViewCommercial.DataBind();

                // loop trough the commercials and hide those that did not get picked for display
                foreach (GridViewRow row in GridViewCommercial.Rows)
                {
                    if (row.RowIndex != randomcommercialToDisplayPosition)
                    {
                        row.Style.Add("display", "none");
                    }
                }

           


        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
           FourthProjectLogic.SearchMovies(TextBoxSearch.Text);
        }
    }
}