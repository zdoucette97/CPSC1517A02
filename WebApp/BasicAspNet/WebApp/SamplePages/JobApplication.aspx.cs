using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class JobApplication : System.Web.UI.Page
    {
        //NO DB, thus a temporary List<t> wher T is the JobApps
        public static List<JobApps> AppList;
        protected void Page_Load(object sender, EventArgs e)
        {
            Message.Text = "";
            if(!Page.IsPostBack)
            {
                AppList = new List<JobApps>();
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            //clear the form controls
            FullName.Text = "";
            EmailAddress.Text = "";
            PhoneNumber.Text = "";
            FullOrPartTime.ClearSelection();
            Jobs.ClearSelection();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //gather the entered data 
            string fullname = FullName.Text;
            string email = EmailAddress.Text;
            string phonenumber = PhoneNumber.Text;
            string time = FullOrPartTime.SelectedValue;
            
            //create a message string containing the data
            string msg = string.Format("Name: {0} Email: {1} Phone: {2} Time: {3}",
                                        fullname, email, phonenumber, time);

            //to handle the checkbox list, traverse the list and obtain the data that was selected
            //after the traverse, add the string of jobs OR an error message to the other message data string
            string jobs = " Jobs: ";
            bool found = false;
            foreach(ListItem placeholderitem in Jobs.Items)
            {
                if(placeholderitem.Selected)
                {
                    found = true;
                    jobs += placeholderitem.Value + " ";

                }

            }
            if (!found)
            {
                //no job requested
                jobs += " you did not select a job. Application rejected.";
            }
            else
            {
                //list of jobs requested, save application
                AppList.Add(new JobApps(FullName.Text, email, phonenumber, time, jobs));
            }

            //display the message string in the output message control 
            Message.Text = msg + jobs;

            //display all collected applications
            JobApplicationList.DataSource = AppList; //only assigns the data
            JobApplicationList.DataBind(); //physical display of the data

        }
    }
}