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
        //CENTRALIZED DEVELOPER DATABASE CONNECTION QUICK-SETUP
        //add or uncomment your own connection and comment out the previous - dont just change the current one to your own ya sonsofabitches
        //to create a new connection simply create the connectionstring in the webconfig and put its name here - that way we only have to change one thing between devs

        //needless to say, if you commit code that does not use this to the git i will flay you alive.   what i will do when i inevitaly do this myself i know not. its sorta like the reverse of do as i say not as i do - these stern warnings are as much for myself as for anyone else  -ask
        //but feel free to use your constring to set up webcontrols that won't play nice with ConMan, the dynamicism is too much for them i guess.

        public static string ConnStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringAndreasHome"].ToString();
        //public static string ConnStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringAndreas"].ToString();
        //public static string ConnecStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringMihnea"].ToString();
        //public static string ConnecStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringStan"].ToString();
        //public static string ConnecStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringSebastian"].ToString();
        //public static string ConnecStr = ConfigurationManager.ConnectionStrings["MovieDBListConnectionStringRazvanHome"].ToString();


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
            }
            return theSqlCommand;

        }
    }
}