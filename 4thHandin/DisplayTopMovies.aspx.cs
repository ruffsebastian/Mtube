using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace _4thHandin
{
    public partial class DisplayTopMovies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = ProjectLogic.ConnStr;
            SqlDataSource1.SelectCommand = "MovieSelectTop10Query";
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

        }
    }
}