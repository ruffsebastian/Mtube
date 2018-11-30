using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _4thHandin
{
    public class ConMan
    {
        //public static SqlConnection ConnecStr = new SqlConnection(@"data source = ANDREAS2018-PC\SQLEXPRESS; integrated security = true; database = Exterminatus;");
        public static SqlConnection ConnecStr = new SqlConnection(@"data source = LISBET; integrated security = true; database = MovieDBList;");
        //public static SqlConnection ConnecStr = new SqlConnection(@"data source = MIHNEA-LAPTOP; integrated security = true; database = MovieDBList;");
        //public static SqlConnection ConnecStr = new SqlConnection(@"data source = THE-STAN; integrated security = true; database = MovieDBList;");
        //public static SqlConnection ConnecStr = new SqlConnection(@"data source = DESKTOP-SCRFL8B; integrated security = true; database = MovieDBList;");

        //add your own connection for easy access and comment out any others
    }
}