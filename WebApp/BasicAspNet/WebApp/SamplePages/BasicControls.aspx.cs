using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class BasicControls : System.Web.UI.Page
    {
        //global variable available to the entire page and will
        //temporarily represent data from the database
        public static List<DDLClass> DataCollection; //if you take out static, everyone has their own version of the page. with static, one copy for everyone
            
        protected void Page_Load(object sender, EventArgs e)
        {
            //this event will happen EACH AND EVERY time your page is executed
            //this event will happen BEFORE ANY of YOUR control events happen
            //this is a great place to initilize items(controls) that are common
            //to many events and need to be done every time

            //e.x.: old messages should be cleared out on every pass
            //      you can empty the MessageLabel control under EACH event
            //   OR you can do it ONCE here for ALL events
            MessageLabel.Text = "";

            //this is a great place to do 1st time initialization of controls
            // (similar to the else side of the IsPost from Razor)

            if (!Page.IsPostBack) //same thing as (Page.IsPostBack == false) or (Page.IsPostBack != true)
            {
                //load the DDL on the first pass. 
                //create an instance of a collection (List<T>; T == class DDLClass)
                //create 4 record instances for the collection 
                //place the collection into the DDL 
                DataCollection = new List<DDLClass>();
                DataCollection.Add(new DDLClass(1, "COMP1008"));
                DataCollection.Add(new DDLClass(2, "CPSC1517"));
                DataCollection.Add(new DDLClass(3, "DMIT1508"));
                DataCollection.Add(new DDLClass(4, "DMIT2018"));

                //sort the data collection for the ddl by program course name
                //syntax: collectionname.Sort((x,y) => x.fieldname.CompareTo(y.fieldname))
                //collectionname is where your data resides (List<t>)
                //(x,y) represent any two values (records) in your collection at any point in time 
                //=> (lamda) can be thought of as "do the following to x and y" (delegate)
                //our delegate for lamda is comparing x and y on the fieldname 
                // x CompareTo y is an ascending sort 
                // y CompareTo x is a descending sort 
                DataCollection.Sort((y, x) => x.DisplayField.CompareTo(y.DisplayField));

                //steps in loading your DDL 
                //assume that the DataCollection is actually database data 
                // a) assign the data source to the control 
                CollectionChoiceList.DataSource = DataCollection; //collectionchoicelist is id of what was used for the dropdownlist in BasicControls.aspx

                //<option value=somevalue> sometext </option>
                //define what somevalue and sometext is in the data collection
                // b) indicate the display field to the control
                CollectionChoiceList.DataTextField = "DisplayField"; //Non-Object Style

                // c) indicate the value field to the control
                CollectionChoiceList.DataValueField = nameof(DDLClass.ValueField); //Object Style; preferable because you can use intellisense

                // d) physically bind the data to the control 
                CollectionChoiceList.DataBind();

                //optional prompt line??
                CollectionChoiceList.Items.Insert(0, "select ..."); //Index for COMP1008 changes from 3 to 4?

            }
        }

        

        protected void SubmitNumberChoice_Click(object sender, EventArgs e)
        {
            //to grab the contents of a control will DEPEND on the cntrol access type
            // for TextBox, label, or literal use .text
            // for List (RadiobuttonList, DropDownList) you may use one of:
            //  .SelectedValue(best), .SelectedIndex(phyical location), .SelectedItem.Text
            // for CheckBox use .Checked (boolean)

            //for the most part, all data from a control returns as a string
            //since the control (object) is on the "right" side of an assignment (set;), statement, the object Property uses its (get;)
            string submitchoice = NumberChoice.Text;

            if(string.IsNullOrEmpty(submitchoice))
            {
                //"left" side uses the Property's SET
                MessageLabel.Text = "You did not enter a value for your program choice";
                ChoiceList.ClearSelection();
                //ChoiceList.SelectedIndex = -1; //-1 is a non-existent index. similar, but not necessary
                //CollectionChoiceList.ClearSelection();
                CollectionChoiceList.SelectedIndex = 0; //0 has my prompt
                AlterLabel.ForeColor = System.Drawing.Color.Black;
                DisplayDataRO.Text = "";

            }
            else
            {
                // you can set/get the radiobuttonlist choice by either using 
                // .SelectedValue or .SelectedIndex or SelectedItem.Text
                // it is BEST to use .SelectedValue for positioning (go an find the line with the proper value and display as desired)
                // using .SelectedIndex requires you know the exact physical location
                ChoiceList.SelectedValue = submitchoice;
                if (submitchoice.Equals("2") || submitchoice.Equals("4"))
                {
                    ProgrammingCourseActive.Checked = true;
                    AlterLabel.ForeColor = System.Drawing.Color.AliceBlue;
                }
                else
                {
                    ProgrammingCourseActive.Checked = false;
                    AlterLabel.ForeColor = System.Drawing.Color.Black;
                }

                //DDL an be positioned using
                // .SelectedValue or .SelectedIndex or SelectedItem.Text
                // it is BEST to use .SelectedValue for positioning 
                CollectionChoiceList.SelectedValue = submitchoice;

                //demonstration of what is obtained by the different by the .SelectedIndex or SelectedItem.Text
                DisplayDataRO.Text = CollectionChoiceList.SelectedItem.Text
                    + " at index " + CollectionChoiceList.SelectedIndex
                    + " having a value of " + CollectionChoiceList.SelectedValue;
            }


        }

        protected void CollectionSubmit_Click(object sender, EventArgs e)
        {
            MessageLabel.Text = "You pressed the Link Button Selection Submit";
        }

        //IF YOU ACCIDENTALLY CLICK ON A METHOD YOU DID NOT WANT, YOU MUST REMOVE IT HERE AND ALSO DELETE IT ON THE ASPX FORM IN THE SOURCE TAG
    }
}