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
            Labelcurrentconman.Text = ConMan.ConnStr;  //tell the dev what connection he is using
            
            //Display a Chuck Norris joke via API about a random teacher
            WebClient client = new WebClient();

            string[] Teachers = { "Torben", "Tue", "Morten", "Jesper" };
            string reply = client.DownloadString("http://api.icndb.com/jokes/random?firstName=" + Teachers[new Random().Next(0, Teachers.Length)]);

            string[] separatingChars = { "\"" };   
            string[] mysplit = reply.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            if (mysplit[0] != "False")
            {
                LabelJoke.Text = mysplit[11]; //i feel a bit dirty but hey, results are results right? [no they are not]
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("./search/?queryName=" + TextBoxSearch.Text);
        }
    }
}