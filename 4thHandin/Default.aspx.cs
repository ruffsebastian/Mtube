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
            WebClient client = new WebClient();            

            string reply = client.DownloadString("http://api.icndb.com/jokes/random?firstName=Torben");
            
            // A simple example. Treat json as a string
            string[] separatingChars = { "\""};
            string[] mysplit = reply.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            //LabelJoke.Text = mysplit.Length.ToString();
            if (mysplit[0] != "False")
            {
                LabelJoke.Text = mysplit[11];
            }
        }
    }
}