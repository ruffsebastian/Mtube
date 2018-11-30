﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace _4thHandin
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LabelResultTitle.Visible = false;
            LabelResultRating.Visible = false;
            LabelResultYear.Visible = false;
            LabelResultActors.Visible = false;
            LabelResultDescription.Visible = false;
            LabelResultChildRating.Visible = false;
            LabelMessages.Visible = false;
        }
        protected void ButtonSearchName_Click(object sender, EventArgs e)

        {

            string result = OmdbAPI.NameAPI(TextBoxInput.Text);

            File.WriteAllText(Server.MapPath("~/MyFiles/Latestresult.xml"), result);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);

            if (doc.SelectSingleNode("/root/@response").InnerText == "True")
            {
                XmlNodeList nodelist = doc.SelectNodes("/root/movie");
                foreach (XmlNode node in nodelist)
                {
                    string poster = node.SelectSingleNode("@poster").InnerText;
                    ImagePoster.ImageUrl = poster;
                }

                LabelResultTitle.Text = "Title: " + nodelist[0].SelectSingleNode("@title").InnerText;
                LabelResultRating.Text = "Rating: " + nodelist[0].SelectSingleNode("@imdbRating").InnerText;
                LabelResultYear.Text = "Year: " + nodelist[0].SelectSingleNode("@year").InnerText;
                LabelResultActors.Text = "Actors: " + nodelist[0].SelectSingleNode("@actors").InnerText;
                LabelResultDescription.Text = "Description: " + nodelist[0].SelectSingleNode("@plot").InnerText;
                LabelResultChildRating.Text = "Child Rating: " + nodelist[0].SelectSingleNode("@rated").InnerText;

                LabelResultTitle.Visible = true;
                LabelResultRating.Visible = true;
                LabelResultYear.Visible = true;
                LabelResultActors.Visible = true;
                LabelResultDescription.Visible = true;
                LabelResultChildRating.Visible = true;

            }
            else
            {
                LabelMessages.Text = "Movie not found";
                LabelMessages.Visible = true;
                ImagePoster.ImageUrl = "~/Myfiles/default-img.png";
                LabelResultTitle.Text = "Result";


            }





            SqlConnection con1 = new SqlConnection(@"data source = DESKTOP-SCRFL8B; integrated security = true; database = MovieDBList;");
            DataTable dt = new DataTable();
            con1.Open();
            SqlDataReader myReader = null;
            // we need to add the second part of sql command on pageload of single page!!!
            SqlCommand myCommand = new SqlCommand("select * from MovieDBList where Title = '"+TextBoxInput.Text+"' update MovieDBList set ViewCount = ViewCount + 1 where Title = '"+TextBoxInput.Text+"'", con1);

            myReader = myCommand.ExecuteReader();
            

            while (myReader.Read())
            {
                LabelMessages.Text = (myReader["Title"].ToString());
                LabelMessages.Visible = true;
                //and whatever you have to retrieve
            }
            con1.Close();

        }

        protected void GridViewMovies_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
    }