using Assignment4_.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Assignment4_.Admin
{
    public partial class Admin : System.Web.UI.Page
    {

        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\hagen\\OneDrive - North Dakota University System\\Software\\Assignment4\\Assignment4_\\App_Data\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dbcon;


        protected void Page_Load(object sender, EventArgs e)
        {
            ShowAllRecords();
        }
        public void ShowAllRecords()
        {
            dbcon = new KarateSchoolDataContext(conn);
            var member = (from members in dbcon.Members
                          select new
                          {
                              members.MemberFirstName,
                              members.MemberLastName,
                              members.MemberPhoneNumber,
                              members.MemberDateJoined,
                              members.Member_UserID,
                              members.MemberEmail
                          });
            GridView1.DataSource = member;
            GridView1.DataBind();

            var instructor = (from instructors in dbcon.Instructors
                              select new
                              {
                                  instructors.InstructorFirstName,
                                  instructors.InstructorLastName,
                                  instructors.InstructorPhoneNumber,
                                  instructors.InstructorID

                              });
            GridView2.DataSource = instructor;
            GridView2.DataBind();
        }
      

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
              
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
           
            NetUser user = new NetUser();
            user.UserName = txtUserName.Text;
            user.UserPassword = txtPassword.Text;
            user.UserID = Convert.ToInt32(txtInstructorID.Text);
            user.UserType = "Instructor";
            dbcon.NetUsers.InsertOnSubmit(user);
            dbcon.SubmitChanges();
            
            




            Instructor instruct = new Instructor();
            instruct.InstructorID = Convert.ToInt32(txtInstructorID.Text);
            instruct.InstructorFirstName = txtFirst.Text;
            instruct.InstructorLastName = txtLast.Text;
            instruct.InstructorPhoneNumber = txtPhone.Text;         
            dbcon.Instructors.InsertOnSubmit(instruct);
            dbcon.SubmitChanges();
            ShowAllRecords();

            




        }

        protected void txtFirst_TextChanged(object sender, EventArgs e)
        {
            
        }
        int userDelete;
       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int delete = Convert.ToInt32(txtDelete.Text);
            userDelete = delete;
            var itemRemove = (from record in dbcon.Instructors
                              where record.InstructorID == delete
                              select record).FirstOrDefault();

            if (itemRemove != null)
            {
                dbcon.Instructors.DeleteOnSubmit(itemRemove);
                try
                {
                    dbcon.SubmitChanges();
                }
                catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            }

            var remove = (from record in dbcon.NetUsers
                          where record.UserID == userDelete
                          select record).FirstOrDefault();
            dbcon.NetUsers.DeleteOnSubmit(remove);
            try
            {
                dbcon.SubmitChanges();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            txtDelete.Text = "";
            ShowAllRecords() ;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            NetUser User = new NetUser();
            User.UserName = txtUser.Text;
            User.UserPassword = txtPass.Text;
            User.UserType = "Member";
            dbcon.NetUsers.InsertOnSubmit(User);
            dbcon.SubmitChanges();




            Member mem = new Member();
            mem.Member_UserID = User.UserID;
            mem.MemberFirstName = txtFirstNameM.Text;
            mem.MemberLastName = txtLastName.Text;
            mem.MemberPhoneNumber = txtPhone.Text;
            mem.MemberDateJoined = Convert.ToDateTime(txtJoined.Text);
            mem.MemberEmail = txtEmail.Text;
            dbcon.Members.InsertOnSubmit(mem);
            dbcon.SubmitChanges();
            ShowAllRecords();

        }

        protected void bteDelete1_Click(object sender, EventArgs e)
        {
            int member = Convert.ToInt32(txtID.Text);
            userDelete = member;
            var itemRemove = (from record in dbcon.Members
                              where record.Member_UserID == member
                              select record).FirstOrDefault();

            if (itemRemove != null)
            {
                dbcon.Members.DeleteOnSubmit(itemRemove);
                try { 
                dbcon.SubmitChanges();
            }
                catch(Exception ex) { Console.WriteLine(ex.Message);
                }
                

                

            }
            ShowAllRecords();

            var remove = (from record in dbcon.NetUsers
                          where record.UserID == userDelete
                          select record).FirstOrDefault();
            dbcon.NetUsers.DeleteOnSubmit(remove);
            try
            {
                dbcon.SubmitChanges();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            txtID.Text = "";
            
        }

        protected void btnSection_Click(object sender, EventArgs e)
        {
            var changes =
    from x in dbcon.Sections
    where x.SectionID == Convert.ToInt32(txtSection.Text)  //change '4' to whatever sectionID is inputted
    select x;

            //change the member from selected section
            foreach (var y in changes)
            {
                y.Member_ID = Convert.ToInt32(MemberID.Text); //change '4' to whatever memberID is to be added

            }

            //try to submit changes
            try
            {
                dbcon.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}