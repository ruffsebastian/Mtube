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
            LabelResultTitle.Visible = false;
            LabelResultRating.Visible = false;
            LabelResultYear.Visible = false;
            LabelResultActors.Visible = false;
            LabelResultDescription.Visible = false;
            LabelResultChildRating.Visible = false;
            LabelMessages.Visible = false;


            string queryID = Request.QueryString["queryID"].ToString();
            SqlConnection con = new SqlConnection(ConMan.ConnStr);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from MovieDBList where id = " + Int32.Parse(queryID) 
                + " update MovieDBList set ViewCount = ViewCount + 1 where ID = " + Int32.Parse(queryID), con);

            myReader = myCommand.ExecuteReader();
            //-----------------------------------------------------------------------------------------------// 

            //get movie name from database and show in label
            while (myReader.Read())
            {
                LabelMessages.Text = (myReader["Title"].ToString());
                LabelMessages.Visible = true;
                //and whatever you have to retrieve
            }

            string result = OmdbAPI.NameAPI(LabelMessages.Text);

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
                ImagePoster.ImageUrl = "/Myfiles/default-img.jpg";
                LabelResultTitle.Text = "no Results";
            }
        }
    }
}