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
        //CENTRALIZED DEVELOPER DATABASE CONNECTION
        public static string ConnStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionString"].ToString();

        //SQL Central
        public static string CommmStr(string theListWeWant, string whatWeWantToFind)
        {
            string theSqlCommand = "";
            switch (theListWeWant)
            {
                case "search":
                    theSqlCommand = "SELECT * FROM MovieDBList WHERE id = '" + whatWeWantToFind + "'";
                    break;
                case "category":
                    theSqlCommand = "SELECT * FROM MovieDBList WHERE category = '" + whatWeWantToFind + "'";
                    break;
                case "year": //why not?
                    theSqlCommand = "SELECT * FROM MovieDBList WHERE year = '" + whatWeWantToFind + "'";
                    break;
                case "top10":
                    theSqlCommand = "SELECT TOP (10) * FROM MovieDBList ORDER BY ViewCount DESC, Year DESC";
                    break;
                case "distinct":
                    break;

            }
            return theSqlCommand;

        }
    }
}