using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _4thHandin
{
    public class ConMan
    {
        //centralized database access point - add or uncomment your own connection and comment out the previous - dont just change the current one to your own
        //to create a new connection simply create it in the webconfig(manually or by setting up a sqldatasource for it and then deleting it) and put its name here 

        public static string ConnecStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringAndreasHome"].ToString();
        //public static string ConnecStr = new SqlConnection(ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringAndreas"].ToString();
        //public static string ConnecStr = new SqlConnection(ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringMihnea"].ToString();
        //public static string ConnecStr = new SqlConnection(ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringStan"].ToString();
        //public static string ConnecStr = new SqlConnection(ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringSebastian"].ToString();
    }
}