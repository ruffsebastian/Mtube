﻿using System;
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
            SqlDataSource1.ConnectionString = ConMan.ConnStr;
            SqlDataSource1.SelectCommand = ConMan.CommmStr("top10","");


            //HyperLink ShowMovie = (HyperLink)e.Item.FindControl("ShowMovie");

            //LinkButton ShowMovie = ;
        }
    }
}