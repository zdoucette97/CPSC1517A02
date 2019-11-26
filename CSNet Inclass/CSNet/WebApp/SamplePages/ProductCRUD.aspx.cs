using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using NorthwindSystem.BLL;
using NorthwindSystem.Data;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

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

        protected void Clear_Click(object sender, EventArgs e)
        {
            ProductID.Text = "";
            ProductName.Text = "";
            QuantityPerUnit.Text = "";
            UnitPrice.Text = "";
            UnitsInStock.Text = "";
            UnitsOnOrder.Text = "";
            ReorderLevel.Text = "";
            ProductList.SelectedIndex = 0;
            CategoryList.SelectedIndex = 0;
            SupplierList.SelectedIndex = 0;
            Discontinued.Checked = false;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            if (ProductList.SelectedIndex == 0)
            {
                errormsgs.Add("Select a product to maintain.");
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                //standrd lookup
                try
                {
                    ProductController sysmgr = new ProductController();
                    Product info = null;
                    info = sysmgr.Products_FindByID(int.Parse(ProductList.SelectedValue));
                    
                    if (info == null)
                    {
                        errormsgs.Add("No longer on file.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");

                        //optionally you can refresh your page to clear the fields
                        Clear_Click(sender, e);
                        //and then reload the binding of the product list
                        BindProductList();

                    }
                    else
                    {
                        ProductID.Text = info.ProductID.ToString();
                        ProductName.Text = info.ProductName.ToString();
                        QuantityPerUnit.Text =
                            info.QuantityPerUnit == null? "" : info.QuantityPerUnit; //nullable field
                        UnitPrice.Text =
                            info.UnitPrice.HasValue ? string.Format("{0:0.00}", info.UnitPrice.Value) : ""; // nullable numeric field formatted
                        UnitsInStock.Text =
                            info.UnitsInStock.HasValue ? info.UnitsInStock.Value.ToString() : ""; // nullable numeric field
                        UnitsOnOrder.Text =
                            info.UnitsOnOrder.HasValue ? info.UnitsOnOrder.Value.ToString() : ""; 
                        ReorderLevel.Text =
                            info.ReorderLevel.HasValue ? info.ReorderLevel.Value.ToString() : ""; 
                        Discontinued.Checked = info.Discontinued;

                        if(info.CategoryID.HasValue)
                        {
                            CategoryList.SelectedValue = info.CategoryID.Value.ToString();
                        }
                        else
                        {
                            CategoryList.SelectedIndex = 0; //set to the "select..."
                        }

                        if (info.SupplierID.HasValue)
                        {
                            SupplierList.SelectedValue = info.SupplierID.Value.ToString();
                        }
                        else
                        {
                            SupplierList.SelectedIndex = 0; //set to the "select..."
                        }
                    }
                    


                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }

            }
        }

        protected void AddProduct_Click(object sender, EventArgs e)
        {
            //recheck validation
            if(Page.IsValid)
            {
                //check any event code validation

                try
                { 
                    //examples:
                    //e.x.1 assume that the category ID is required
                    if (CategoryList.SelectedIndex == 0) //still on prompt
                    {
                        errormsgs.Add("No category selected.");
                    }
                    //e.x.2 check the string length of QuantityPerUnit
                    if (QuantityPerUnit.Text.Length > 20)
                    {
                        errormsgs.Add("QuantityPerUnit is limited to 20 characters.");
                    }
                    //      Is data still good?
                    if (errormsgs.Count > 0)
                    {
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                    }
                    else
                    {
                        //assume at this point, the data is good! you are ready for processing

                        //Standard Add Pattern:
                        //1) connect to the controller
                        ProductController sysmgr = new ProductController();

                        //2) create and load an instance of the entity
                        //      since there was no constructor placed in the entity, 
                        //      when one creates the instance the default system constructor will be used
                        Product item = new Product();
                        //  NOTE: item name isn't important, can be anything
                        //  NOTE: since the ProductID is an identity field it does NOT need to be loaded into the new instance
                        item.ProductName = ProductName.Text.Trim();
                        //      trim removes accidental spaces
                        //  E.X. the long way of dealing with nullable numerics
                        if (CategoryList.SelectedIndex == 0)
                        {
                            item.CategoryID = null;
                        }
                        else
                        {
                            item.CategoryID = int.Parse(CategoryList.SelectedValue);
                        }
                        
                        //  E.X. the short way of dealing with nullable numerics
                        item.SupplierID =
                            SupplierList.SelectedIndex == 0 ? (int?)null : int.Parse(SupplierList.SelectedValue);
                        item.QuantityPerUnit =
                            string.IsNullOrEmpty(QuantityPerUnit.Text) ? null : QuantityPerUnit.Text;
                        item.UnitPrice =
                            string.IsNullOrEmpty(UnitPrice.Text) ? (decimal?)null : decimal.Parse(UnitPrice.Text);
                        item.UnitsInStock =
                            string.IsNullOrEmpty(UnitsInStock.Text) ? (Int16?)null : Int16.Parse(UnitsInStock.Text);
                        item.UnitsOnOrder =
                            string.IsNullOrEmpty(UnitsOnOrder.Text) ? (Int16?)null : Int16.Parse(UnitsOnOrder.Text);
                        item.ReorderLevel =
                            string.IsNullOrEmpty(ReorderLevel.Text) ? (Int16?)null : Int16.Parse(ReorderLevel.Text);
                        //just added; can't be discontinued
                        item.Discontinued = false;

                        //3) issue the BLL call 
                        int newProductID = sysmgr.Products_Add(item);

                        //4) give feedback
                        //      if you get to execute the feedback code, it means that the product has been sucessfully added to  the db
                        ProductID.Text = newProductID.ToString();
                        errormsgs.Add("Product has been added.");
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                        //      is there any other controls on the form that need to be refreshed?
                        //      you want the new product item to be added to the list!
                        BindProductList();
                        //  NOTE: by default, List will be at index 0 (the prompt). ideally, you want it to be set to the new item
                        ProductList.SelectedValue = ProductID.Text; //or newProductID.ToString(), but this text string is better

                    }


                }
                catch(Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbUpdateException ex)
                {
                    UpdateException updateException = (UpdateException)ex.InnerException;
                    if (updateException.InnerException != null)
                    {
                        errormsgs.Add(updateException.InnerException.Message.ToString());
                    }
                    else
                    {
                        errormsgs.Add(updateException.Message);
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            errormsgs.Add(validationError.ErrorMessage);
                        }
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }

            }

        }

        protected void UpdateProduct_Click(object sender, EventArgs e)
        {
            //recheck validation
            if (Page.IsValid)
            {
                //check any event code validation

                try
                {
                    //examples:
                    //e.x.1 assume that the category ID is required
                    if (CategoryList.SelectedIndex == 0) //still on prompt
                    {
                        errormsgs.Add("No category selected.");
                    }
                    //e.x.2 check the string length of QuantityPerUnit
                    if (QuantityPerUnit.Text.Length > 20)
                    {
                        errormsgs.Add("QuantityPerUnit is limited to 20 characters.");
                    }
                    //  On update ensure you have your PK
                    int productid = 0;
                    if (string.IsNullOrEmpty(ProductID.Text))
                    {
                        errormsgs.Add("Product ID is invalid.");
                    }
                    else if (!int.TryParse(ProductID.Text, out productid))
                    {
                        errormsgs.Add("Product ID is invalid.");
                    }
                    else if (productid < 1)
                    {
                        errormsgs.Add("Product ID is invalid.");
                    }
                    
                    //  Is data still good?
                    if (errormsgs.Count > 0)
                    {
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                    }
                    else
                    {
                        //assume at this point, the data is good! you are ready for processing

                        //Standard Add Pattern:
                        //1) connect to the controller
                        ProductController sysmgr = new ProductController();

                        //2) create and load an instance of the entity
                        //      since there was no constructor placed in the entity, 
                        //      when one creates the instance the default system constructor will be used
                        Product item = new Product();
                        item.ProductID = productid;
                        //  NOTE: item name isn't important, can be anything
                        //  NOTE: since the ProductID is an identity field it does NOT need to be loaded into the new instance
                        item.ProductName = ProductName.Text.Trim();
                        //      trim removes accidental spaces
                        //  E.X. the long way of dealing with nullable numerics
                        if (CategoryList.SelectedIndex == 0)
                        {
                            item.CategoryID = null;
                        }
                        else
                        {
                            item.CategoryID = int.Parse(CategoryList.SelectedValue);
                        }

                        //  E.X. the short way of dealing with nullable numerics
                        item.SupplierID =
                            SupplierList.SelectedIndex == 0 ? (int?)null : int.Parse(SupplierList.SelectedValue);
                        item.QuantityPerUnit =
                            string.IsNullOrEmpty(QuantityPerUnit.Text) ? null : QuantityPerUnit.Text;
                        item.UnitPrice =
                            string.IsNullOrEmpty(UnitPrice.Text) ? (decimal?)null : decimal.Parse(UnitPrice.Text);
                        item.UnitsInStock =
                            string.IsNullOrEmpty(UnitsInStock.Text) ? (Int16?)null : Int16.Parse(UnitsInStock.Text);
                        item.UnitsOnOrder =
                            string.IsNullOrEmpty(UnitsOnOrder.Text) ? (Int16?)null : Int16.Parse(UnitsOnOrder.Text);
                        item.ReorderLevel =
                            string.IsNullOrEmpty(ReorderLevel.Text) ? (Int16?)null : Int16.Parse(ReorderLevel.Text);
                        //for an update, you will want to take the current value of discontinued
                        item.Discontinued = Discontinued.Checked;


                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~THIS IS WHERE I GOT LOST YOOOOOOOOOOOOOOOOOOOOOOOOO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        //3) issue the BLL call 
                        int newProductID = sysmgr.Products_Add(item);

                        //4) give feedback
                        //      if you get to execute the feedback code, it means that the product has been sucessfully added to  the db
                        ProductID.Text = newProductID.ToString();
                        errormsgs.Add("Product has been added.");
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                        //      is there any other controls on the form that need to be refreshed?
                        //      you want the new product item to be added to the list!
                        BindProductList();
                        //  NOTE: by default, List will be at index 0 (the prompt). ideally, you want it to be set to the new item
                        ProductList.SelectedValue = ProductID.Text; //or newProductID.ToString(), but this text string is better

                    }


                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbUpdateException ex)
                {
                    UpdateException updateException = (UpdateException)ex.InnerException;
                    if (updateException.InnerException != null)
                    {
                        errormsgs.Add(updateException.InnerException.Message.ToString());
                    }
                    else
                    {
                        errormsgs.Add(updateException.Message);
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            errormsgs.Add(validationError.ErrorMessage);
                        }
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }

            }

        }

        protected void RemoveProduct_Click(object sender, EventArgs e)
        {

        }
    }

}