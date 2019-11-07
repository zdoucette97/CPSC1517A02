using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace NorthwindSystem.Data
{
    //all classes by efault are pivate, change to public
    //all entity ir data classes in this course will be singular by name

    //an annotation that will point this class to the appropriate sql tabl is needed in front of the class header
    //an annotation syntax is [annotation]
    //syntax [Table{"mysqltablename"[,Schema="sqlschemaname"])]
    //sometimes your sql database will be divided into groups called schemas
    //      you can recognize a schema on a table by the name it is using:
    //      i.e: HumanResources.Employees
    //IF your database does NOT have schemas, you omit the schema attribute from your annotation
    //      i.e. 
    //the creation of this class is called mapping
    [Table("Products")]
    public class Product
    {
        //all sql attributes will have a corresponding class property
        //IF you use the attribute name as your property name
        //  the physical order of the properties do NOT need to match the sql attribute order

        //you need a [Key] annotation for your primary key field
        //[Key] use on an identity pkey field (default)
        //[Key, column(order=n)] use on compound pkey fields where: 
        //      n represents the physical order of the components.
        //      n starts at 1 (natural number)
        //[Key, DataGenerated(DataGeneratedOption.None)] use for 
        //  pkeys that are NOT compound OR NOT identity; that is the user must supply the pkey value

        [Key]

        public int ProductID { get; set; }
        public string ProductName { get; set; }

        //[ForeignKey] DO NOT USE 
        // optional annotation, use ONLY IF your foreign key is NOT the same as the associated primary key field name
        //  or you use a different name in your mapping

        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        //optionally add your constructors

        //other annotation 
        //computedfield does exist on the database table
        //this field does NOT expect data from the user nor does
        //  EntityFramework expect data to be passed to this sql attribute

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public decimal Total {get; set;}

        //Read-Only application property
        //for example, lets say you would like to concatenate some fields together
        //  within your application on several occassions such as 
        //  creating a fullname out of two attributes (Firstname, Lastname)

        //these read-only properties 
        public string ProductandID
        {
            get
            {
                return ProductName + "(" + ProductID + ")";
            }
        }
    }
}
