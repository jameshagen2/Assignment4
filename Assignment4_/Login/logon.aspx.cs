using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4_.Login
{
    public partial class logon : System.Web.UI.Page
    {

        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hagen\\OneDrive - North Dakota University System\\Software\\Assignment4\\Assignment4_\\App_Data\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dbcon;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            dbcon = new KarateSchoolDataContext(connString);


            string userName = Login1.UserName;
            string password = Login1.Password;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT UserType FROM NetUser WHERE UserName = '" + userName + "'", conn);
                string type = cmd.ExecuteScalar().ToString();

                if (type == "Instructor")
                {
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Response.Redirect("InstructorPage.aspx");
                }

                if (type == "Member")
                {
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Response.Redirect("MemberPage.aspx");

                    
                }
                if (type == "Administrator")
                {
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Response.Redirect("Admin.aspx");
                }

                conn.Close();

            }
        
        }
    }
}