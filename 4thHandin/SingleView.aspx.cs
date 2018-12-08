using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace _4thHandin
{
    public partial class SingleView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //redirect to home if there is no querystring
            if (string.IsNullOrWhiteSpace(Request.QueryString["queryID"])) {
                Response.Redirect("~/");
            }
                   
            resultspanel.Visible = false;

            string queryID = Request.QueryString["queryID"].ToString();

            FourthProjectLogic.Movie themovie = new FourthProjectLogic.Movie(queryID); //get movie object via id, fetching db info for it

            themovie.IncrementViewcount2(); //what it says on the tin, this is how we track views

            //get movie name from object and show in label
            LabelMessages.Text = themovie.ToString();
            LabelMessages.Visible = true;
            
            string result = FourthProjectLogic.OmdbAPI.NameAPI(themovie.title);

            File.WriteAllText(Server.MapPath("~/MyFiles/Latestresult.xml"), result);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);

            if (doc.SelectSingleNode("/root/@response").InnerText == "True")
            {
                XmlNodeList nodelist = doc.SelectNodes("/root/movie");
                foreach (XmlNode node in nodelist)
                {
                    string poster = node.SelectSingleNode("@poster").InnerText;
                    if (poster == "N/A" || poster == "FAIL")
                    {

                        ImagePoster.ImageUrl = "/Myfiles/default-img.jpg";
                    }
                    else
                    {

                    ImagePoster.ImageUrl = poster;
                    }
                }

                LabelResultTitle.Text = "Title: " + nodelist[0].SelectSingleNode("@title").InnerText;
                LabelResultRating.Text = "Rating: " + nodelist[0].SelectSingleNode("@imdbRating").InnerText;
                LabelResultYear.Text = "Year: " + nodelist[0].SelectSingleNode("@year").InnerText;
                LabelResultActors.Text = "Actors: " + nodelist[0].SelectSingleNode("@actors").InnerText;
                LabelResultDescription.Text = "Description: " + nodelist[0].SelectSingleNode("@plot").InnerText;
                LabelResultChildRating.Text = "Child Rating: " + nodelist[0].SelectSingleNode("@rated").InnerText;

                resultspanel.Visible = true;
            }
            else
            {
                resultspanel.Visible = true;
                LabelResultTitle.Visible = false;
                LabelResultRating.Visible = false;
                LabelResultYear.Visible = false;
                LabelResultActors.Visible = false;
                LabelResultDescription.Visible = false;
                LabelResultChildRating.Visible = false;

                LabelMessages.Text = "Movie not found";
                LabelMessages.Visible = true;
                ImagePoster.ImageUrl = "/Myfiles/default-img.jpg";
                LabelResultTitle.Text = "no Results";
            }
        }
    }
}