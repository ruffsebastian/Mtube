using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace _4thHandin
{
    public class OmdbAPI
    {


        public static string NameAPI(string name)
        {
            WebClient client = new WebClient();
            string result = client.DownloadString("http://www.omdbapi.com/?t=" + name + "&r=xml&apikey=" + TokenClass.token);
            return result;
        }


    }
}