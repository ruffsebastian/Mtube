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

            SqlDataSource1.ConnectionString = ConMan.ConnStr;
            SqlDataSource2.ConnectionString = ConMan.ConnStr;

            //UpdateGridView();
            Panel1.Visible = true;
            Panel2.Visible = true;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = true;
        }
    }
}