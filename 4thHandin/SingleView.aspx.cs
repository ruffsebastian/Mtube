﻿using System;
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
                   
            //resultspanel.Visible = false;

            int queryID = Int32.Parse(Request.QueryString["queryID"]);

            FourthProjectLogic.Movie themovie = new FourthProjectLogic.Movie(queryID); //get movie object via id, fetching db info for it

            themovie.IncrementViewcount(); //what it says on the tin, this is how we track views

            //get movie name from object and show in label
            LabelMessages.Text = themovie.ToString();
            LabelMessages.Visible = true;
            
            string result = FourthProjectLogic.OmdbAPI.NameAPI(themovie.title,themovie.year); //add year to make sure we get correct results?

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
                LabelMessages.Visible = false;
                //resultspanel.Visible = true;
            }
            else
            {
                //resultspanel.Visible = true;
                LabelMessages.Visible = true;
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


            // COMMERCIAL          
            // run the logic for commercial stat tracking - reading and modifying the xml to increment viewcount for the random commercial and passing the commercials id/rowindex/"position"

            int randomcommercialToDisplayPosition = FourthProjectLogic.Commercials.StatTracker();

            // load the xml for display 


            DataSet ds = new DataSet();
            ds.ReadXml(MapPath("/xml/commercialsTransformedTemp.xml"));
            rpMyRepeater.DataSource = ds;
            rpMyRepeater.DataBind();


            foreach (RepeaterItem item in rpMyRepeater.Items)
            {
                if (item.ItemIndex != randomcommercialToDisplayPosition)
                {
                    item.Visible = false;
                }
            }
        }
    }
}