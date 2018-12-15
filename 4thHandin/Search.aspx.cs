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
using System.Threading;

namespace _4thHandin
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = FourthProjectLogic.ConnStr;
            this.GridViewDisplaySearch.Visible = false;
            if (GridViewDisplaySearch.Rows.Count == 1)
            {
                Response.Redirect("~/SingleView/?queryID=" + GridViewDisplaySearch.Rows[0].Cells[0].Text);
            }
            if (GridViewDisplaySearch.Rows.Count == 0)
            {
                //show no results message 
            }
        }
    }
}

