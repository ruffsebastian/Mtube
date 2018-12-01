using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace _4thHandin
{
    public class OmdbAPI
    {
        private static WebClient client = new WebClient();

        public static string NameAPI(string name)
        {
            
            string result = client.DownloadString("http://www.omdbapi.com/?apikey="+ TokenClass.token + "&r=xml&t=" + name);
            return result;
        }

        internal static string NameAPI(int v)
        {
            throw new NotImplementedException();
        }
    }
}