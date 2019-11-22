using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using NorthwindSystem.DAL;
using NorthwindSystem.Data;
#endregion

namespace NorthwindSystem.BLL
{
    public class ProductController
    {
        //using SqlQuery to do non primary key lookups
        public List<Product> Products_FindByCategory(int categoryid)
        {
            using (var context = new NorthwindContext())
            {
                //syntax
                //context.Database.SqlQuery<T>("sqlprocname [@parameterid[,@parameterid, ...]]"
                //      [, new SqlParameter("parameterid", value)[, new SqlParameter("parameterid",value)]]);
                //examples
                //context.Database.SqlQuery<T>("sqlprocname");  no parameters
                //context.Database.SqlQuery<T>("sqlprocname @parameterid"
                //      , new SqlParameter("parameterid", value)); one parameter
                //context.Database.SqlQuery<T>("sqlprocname @parameterid,@parameterid"
                //      , new SqlParameter("parameterid", value), new SqlParameter("parameterid",value)); +1> parameters

                //the return datatype of this query is IEnumerable<T>
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByCategories @CategoryID"
                        , new SqlParameter("CategoryID", categoryid));
                return results.ToList();
            }
        }

        public List<Product> Products_List()
        {
            using (var context = new NorthwindContext())
            {
                
                return context.Products.ToList();
            }
        }

        public Product Products_FindByID(int productid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Find(productid);
            }
        }

        public int Products_Add(Product item)
        {
            //at some point in time your individual product fields must be placed in an instance of the class
            //this can be done on the webpage or within this method

            //using clause starts with a transaction with ends with a rollback or a commit
            using (var context = new NorthwindContext())
            {
                //1)Stage the data for the execution by the commit statement.
                //  Staging is done in local memory
                //  Stagin DOES NOT create an identity value; this is done at the commit

                context.Products.Add(item);

                //2)Commit your staged record to the database
                //  If the commiting command is successful, then the new identity value WILL exist in your data instance
                //  If the committing command is NOT successful, the transaction is rolled back

                context.SaveChanges();

                //optionally
                //you may decide to return the new identity value to the web page
                //  if you decide to return the value then the method has a return data type of int,
                //  else the method should be using a return data type of void
                //SINCE the commit command worked (if you are executing this statement) you will find the identity value in your instance

                return item.ProductID;
            }
        }
    }
}
