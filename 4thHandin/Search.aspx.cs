using System;
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
        WebClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new WebClient();
            

        }

        protected void ButtonSearchName_Click(object sender, EventArgs e)

        {
            SqlConnection con1 = new SqlConnection(@"data source = MIHNEA-LAPTOP; integrated security = true; database = MovieDBList;");
            DataTable dt = new DataTable();
            con1.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from MovieList where Title  = '" + TextBoxInput.Text + "'", con1);

            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                LabelMessages.Text = (myReader["Title"].ToString());
                
                //and whatever you have to retrieve
            }
            con1.Close();




            /* if (TextBoxInput.Text == "")
                 TextBoxInput.Text = "IT";
             string reply = client.DownloadString("http://localhost:" + TextBoxPort.Text + "/api/values/" + TextBoxInput.Text);
             LabelMessages.Text = reply;
             string result = "";



             if (RadioButtonName.Checked)
             {
                 //substitute space "n" with "+"
                 string myselection = TextBoxInput.Text.Replace(' ', '+');
                 if (RadioButtonJSON.Checked)
                 {
                     result = client.DownloadString("http://www.omdbapi.com/?t=" + myselection + "&apikey=" + TokenClass.token);
                 }

             }


             if (RadioButtonJSON.Checked)
             {
                 File.WriteAllText(Server.MapPath("~/MyFiles/Latestresult.json"), result);

                 //A simple example. Treat Json as a string
                 string[] separatingChars = { "\":\"", "\",\"", "\":[{\"", "\"},{\"", "\"}]\"", "{\"", "\"}" };
                 string[] mysplit = result.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

                 if (mysplit[1] != "False")
                 {
                     LabelMessages.Text = "Movie found :)";
                     for (int i = 0; i < mysplit.Length; i++)
                     {
                         if (mysplit[i] == "Poster")
                         {
                             ImagePoster.ImageUrl = mysplit[++i];
                             break;
                         }
                     }
                     LabelResults.Text = "Ratings : ";
                     for (int i = 0; i < mysplit.Length; i++)
                     {
                         if (mysplit[i] == "Ratings")
                         {
                             while (mysplit[++i] == "Source")
                             {
                                 if (mysplit[i - 1] != "Ratings")
                                 {
                                     LabelResults.Text += ";  ";
                                 }
                                 LabelResults.Text += mysplit[i + 3] + " from " + mysplit[i + 1];
                                 i = i + 3;
                             }
                             break;
                         }

                     }

                 }
                 else
                 {
                     LabelMessages.Text = "Movie not found";
                     ImagePoster.ImageUrl = "~/MyFiles/goat.png";
                     LabelResults.Text = "Result";
                 }*/
        }

        protected void GridViewMovies_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
    }
