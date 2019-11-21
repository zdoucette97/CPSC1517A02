using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApp.SamplePages
{
    public partial class SqlQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BindCategoryList()
        {
            //standard lookup
            try
            {

            }
            catch
            {

            }
        }
        //NEED FETCH CLICK FROM EXAMPLESSSSSSSSSSSSSSSSSSS


        protected void ProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //events usually come with a set of arguments
            //the particular class of arguments are found in the event header
            //different events have different argument classes

            //you must set the gridview PageIndex property to the new page index carried by the arguemnt (e) instance
            ProductList.PageIndex = e.NewPageIndex;

            //you must refresh your data collection and assign it to the control 
            FetchClick(sender, new EventArgs()); //call the method which already has the refresh code

            //send to the recieving page FROM SAMPLE PAGES
        }
    }
}