using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication1.Instructors
{
    public partial class Instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //need the user ID from login. We can add it to Sessions
            //userID example
            //int userID = (int)Session[0];
            int userID = 11; //temporary USERID varible for instructor

            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\logan\\Desktop\\Fall2023\\CSCI 213\\Assignments\\Assignment4_JohnsonLogan\\Assignment4\\CSCI213_Assignment4\\App_Data\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";

            KarateSchoolsDataContext dbcon = new KarateSchoolsDataContext(conn);

            var username = (from instructors in dbcon.Instructors
                            where (instructors.InstructorID == userID)
                            select new
                            {
                                instructors.InstructorFirstName,
                                instructors.InstructorLastName
                            });
            GridView2.DataSource = username;
            GridView2.DataBind();

            var result = (from sections in dbcon.Sections
                          from instructors in dbcon.Instructors
                          from members in dbcon.Members
                          where (

                          //members.Member_UserID == userID &&
                          //members.Member_UserID == sections.Member_ID &&
                          //instructors.InstructorID == sections.Instructor_ID
                          instructors.InstructorID == userID &&
                          instructors.InstructorID == sections.Instructor_ID &&
                          members.Member_UserID == sections.Member_ID


                          )
                          select new
                          {
                              sections.SectionName,
                              members.MemberFirstName,
                              members.MemberLastName
                              
                          });
            GridView1.DataSource = result;
            GridView1.DataBind();
        }
    }
}