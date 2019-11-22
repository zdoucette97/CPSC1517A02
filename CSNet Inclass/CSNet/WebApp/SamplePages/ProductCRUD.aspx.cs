using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using NorthwindSystem.BLL;
using NorthwindSystem.Data;
#endregion

namespace WebApp.NorthwindPages
{
    public partial class ProductCRUD : System.Web.UI.Page
    {
        //this collection of error messages is used by the specialized error handling code (DataList)
        List<string> errormsgs = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //the following  replaces the basic label messagelabel control to clear old messages
            //the control being used for error handling display is DataList
            //the clearing of DataList must then be done instead
            Message.DataSource = null;
            Message.DataBind();

            if (!Page.IsPostBack)
            {
                BindProductList();
                BindSupplierList();
                BindCategoryList();
            }

        }

        //use this method to discover the inner most error message.
        //this rotuing has been created by the user
        protected Exception GetInnerException(Exception ex)
        {
            //drill down to the inner most exception (should be used on all of your programs)
            
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        //use this method to load a DataList with a variable
        //number of message lines.
        //each line is a string
        //the strings (lines) are passed to this routine in
        //   a List<string>
        //second parameter is the bootstrap cssclass
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }

        #region Binding of DropDownLists
        protected void BindProductList()
        {
            try
            {
                ProductController sysmgr = new ProductController();
                List<Product> info = null;
                info = sysmgr.Products_List();
                info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                ProductList.DataSource = info;
                ProductList.DataTextField = nameof(Product.ProductName);
                ProductList.DataValueField = nameof(Product.ProductID);
                ProductList.DataBind();
                ProductList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                //MessageLabel.Text = ex.Message;

                //using the specialized error handling DataList control
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }

        protected void BindSupplierList()
        {
            try
            {
                SupplierController sysmgr = new SupplierController();
                List<Supplier> info = null;
                info = sysmgr.Supplier_List();
                info.Sort((x, y) => x.ContactName.CompareTo(y.ContactName));
                SupplierList.DataSource = info;
                SupplierList.DataTextField = nameof(Supplier.ContactName);
                SupplierList.DataValueField = nameof(Supplier.SupplierID);
                SupplierList.DataBind();
                SupplierList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                
                //using the specialized error handling DataList control
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }

        protected void BindCategoryList()
        {
            
            try
            {
                CategoryController sysmgr = new CategoryController();
                List<Category> info = null;
                info = sysmgr.Categories_List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                CategoryList.DataSource = info;
                CategoryList.DataTextField = nameof(Category.CategoryName);
                CategoryList.DataValueField = nameof(Category.CategoryID);
                CategoryList.DataBind();
                CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }

        #endregion

    }

}