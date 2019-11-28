using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using NorthwindSystem.DAL;
using NorthwindSystem.Data;
using System.ComponentModel;
#endregion

namespace NorthwindSystem.BLL
{
    [DataObject]
    public class ProductController
    {
        //using SqlQuery to do non primary key lookups
        [DataObjectMethod(DataObjectMethodType.Select, false)]
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

        [DataObjectMethod(DataObjectMethodType.Select, false)] //not necessary but shows how to expose other methods
        public List<Product> Products_List()
        {
            using (var context = new NorthwindContext())
            {
                
                return context.Products.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Product Products_FindByID(int productid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Find(productid);
            }
        }


        //Insert

        public int Products_Add(Product item)
        {
            //at some point in time your individual product fields must be placed in an instance of the class
            //this can be done on the webpage or within this method

            //using clause starts with a transaction with ends with a rollback or a commit
            using (var context = new NorthwindContext())
            {
                //1)Stage the data for the execution by the commit statement.
                //  Staging is done in local memory
                //  Staging DOES NOT create an identity value; this is done at the commit

                context.Products.Add(item);

                //2)Commit your staged record to the database
                //  IF there is ANY entity validation annotation, it will be executed during the .SaveChanges processing
                //  IF any entity validation error is discovered the message(s) are returned and the commit is aborted
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

        //Update
        //change the entire entity record
        //it does not matter LOGICALLY that you change a value to itself
        //by changing the entire entity, you change all fields that need to be altered
        //the value returned is the number of rows affected
        public int Products_Update(Product item)
        {
            using(var context = new NorthwindContext())
            {
                //stating 
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                //commit and feeback
                return context.SaveChanges();
            }
        }

        //Delete
        //delete (physical) or change(logical delete really and update) the entire entity record
        //the value returned is the number of rows affected
        public int Products_Delete(int productid)
        {
            using (var context = new NorthwindContext())
            {
                //~~~Physical Delete~~~
                //  the physical removal of a reocrd from the database

                //1) locate the instance of the entity to be removed
                //var existing = context.Products.Find(productid);
                ////2)optionally check to see if it is there
                ////if not throw an exception
                ////this process can also be handled on the wbe form during feedback
                //if (existing == null)
                //{
                //    throw new Exception("Record has been removed from the database.");
                //}
                ////3) staging
                //context.Products.Remove(existing);
                ////4) commit and feedback
                //return context.SaveChanges();

                //~~~Logical Delete~~~
                //  you normally set a property to a specific value to indicate the record should be considered "gone" 
                //  this is actually an update of the record

                //1) locate the instance of the entity to be changed
                var existing = context.Products.Find(productid);
                //2) check to see if record exists
                if (existing == null)
                {
                    throw new Exception("Record has been removed from the database.");
                }
                //3) set the property to the specific value
                existing.Discontinued = true;
                //4) staging
                context.Entry(existing).State = System.Data.Entity.EntityState.Deleted;
                //5) commit and feedback
                return context.SaveChanges();
            }
        }
    }
}
