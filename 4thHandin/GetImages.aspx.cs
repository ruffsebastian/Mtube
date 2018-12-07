using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4thHandin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = ProjectLogic.ConnStr;
            
            SqlConnection con1 = new SqlConnection(ProjectLogic.ConnStr);
            
            int countPoster = 0;
            con1.Open();
            foreach (GridViewRow row in GridViewMovies.Rows)
            {
                SqlCommand myCommand1 = new SqlCommand("update MovieDBList set PosterPath ='" + GetPosterAPI.GetUrl(row.Cells[1].Text) + "' where Title = '" + row.Cells[1].Text + "'", con1);
                myCommand1.ExecuteNonQuery();

                //Thread.Sleep(5000);

                LabelMessages.Visible = true;
                LabelMessages.Text = "last (grid)row affected:" + countPoster.ToString();

                ImagePoster.ImageUrl = GetPosterAPI.GetUrl(row.Cells[1].Text);

            }
            con1.Close();
        }
    }
}