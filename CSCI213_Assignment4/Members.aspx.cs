using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /** Dont know how to implement this quite yet
             * 
            if (Session.Count != 0)
            {
                foreach (var item in Session.Keys)
                {
                    lblFirstName.Text = item + "   " + Session[item.ToString()];
                    lblLastName.Text = item + "   " + Session[item.ToString()];
                }
            }
            */

            string conn = null;

            KarateSchoolsDataContext dbcon = new KarateSchoolsDataContext(conn);

            var result = from x in dbcon.Sections
                         where(true) //need to fix
                         select new
                         {
                             x.SectionName,
                             x.Instructor_ID, //need to change to first and last
                             x.SectionStartDate
                             
                             
                         };
            GridView1.DataSource = result;
            GridView1.DataBind();


        }
    }
}