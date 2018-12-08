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
    public partial class Commercials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // xml to gridview via dataset - now how do i make this crud exactly torben?
            DataSet xmldata = new DataSet();
            xmldata.ReadXml(MapPath("~/xml/commercialsTransformed.xml"));
            GridView2.DataSource = xmldata;
            GridView2.DataBind();
            
            // if the xml uses a namespace the xslt must refer to this namespace   <--- not sure what this means or what the this refers to -ask
            // later note: ah, so thats why we had to prefix all our xlst stuff with c: to make it work... now being that this "c" could be anything why the fuck not call it "ourdummynamespace"? 
            // point being, use names that make sense / inform the person reading the code - its better for everyone. it only kinda makes sense with things like s=string or i=int if the given thing only needs to exist for counting
            // but then why not call it counter? hell, call you could it therandomnumberwegeneratefromzerotothenumberofcommercialswehave and skip comments - bad example but the overall point is valid

            int randomcommercialToDisplayPosition = new Random().Next(0, GridView1.Rows.Count);  // <-- kinda like that

            FourthProjectLogic.CheckCommercials();

            /* get random commercial using a gridview and our random position*/
            foreach (GridViewRow row in GridView1.Rows)
            {         
                if (row.RowIndex != randomcommercialToDisplayPosition)    // if the current rows position in the order of elements/array of objects/gridview of rows is not the same as the random number - hide it
                {
                    row.Style.Add("display", "none"); //this way around to avoid having to set the rows to initially be hidden
                }
                else
                {
                    //FourthProjectLogic.TrackCommercialViews(randomcommercialToDisplayPosition); //increase the viewcount of the commercial in this position
                }
            }
        }
    }
}