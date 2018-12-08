using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4thHandin
{
    public partial class MoviesByCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlDataSource1.ConnectionString = FourthProjectLogic.ConnStr;
            SqlDataSource2.ConnectionString = FourthProjectLogic.ConnStr;
            SqlDataSource5.ConnectionString = FourthProjectLogic.ConnStr;
            //UpdateGridView();
            Panel1.Visible = true;
            Panel2.Visible = true;

           /* foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType ==   )
                {
                    var PosterPath = (Image)item.FindControl("PosterPath");

                    //Do something with your checkbox...
                    PosterPath.ImageUrl = "/Myfiles/default-img.jpg";
                }
            }*/
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = true;
        }
    }
}