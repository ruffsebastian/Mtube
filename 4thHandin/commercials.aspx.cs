using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Xsl;
using System.Data;
using System.Net;


namespace _4thHandin
{
    public partial class commercials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // if the xml uses a namespace the xslt must refer to this namespace   <--- not sure what this means or what the this refers to -ask
            
            string sourcefile = Server.MapPath("xml/commercials.xml");
            string xslfile = Server.MapPath("xml/commercialsXSLT - Copy.xslt");

            string destinationfile = Server.MapPath("xml/commercialsTransformed.xml");

            FileStream fs = new FileStream(destinationfile, FileMode.Create);
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(xslfile);
            xct.Transform(sourcefile, null, fs);
            fs.Close();
            


        }
    }
}