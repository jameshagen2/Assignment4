using Microsoft.Ajax.Utilities;
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
            //need the user ID from login. We can add it to Sessions
            //userID example
            //int userID = (int)Session[0];
            int userID = 6; //temporary USERID varible
            
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\logan\\Desktop\\Fall2023\\CSCI 213\\Assignments\\Assignment4_JohnsonLogan\\Assignment4\\CSCI213_Assignment4\\App_Data\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";

            KarateSchoolsDataContext dbcon = new KarateSchoolsDataContext(conn);

            var username = (from members in dbcon.Members
                            where (members.Member_UserID == userID)
                            select new
                            {
                                members.MemberFirstName,
                                members.MemberLastName
                            });
            GridView2.DataSource = username;
            GridView2.DataBind();

            var result = (from sections in dbcon.Sections
                          from instructors in dbcon.Instructors
                          from members in dbcon.Members
                          where (

                          members.Member_UserID == userID &&
                          members.Member_UserID == sections.Member_ID &&
                          instructors.InstructorID == sections.Instructor_ID

                          )
                          select new
                          {
                              sections.SectionName,
                              instructors.InstructorFirstName,
                              instructors.InstructorLastName,
                              sections.SectionStartDate //assuming this is the payment date?
                              //need to find out what it means for user payment
                          });
            
            GridView1.DataSource = result;
            GridView1.DataBind();


        }
    }
}