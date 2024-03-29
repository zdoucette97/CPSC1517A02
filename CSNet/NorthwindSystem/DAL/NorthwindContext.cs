﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity; //inheritance of DbContext from EntityFramework
using NorthwindSystem.Data; //points to the data definitions of the entities
#endregion

namespace NorthwindSystem.DAL
{
    //security enhancement using access priviledge: internal
    //restricts the access to this class to call from within lthis library project

    //this class needs to be "tied" into EntityFramework
    //this will be done by inheriting the class DbContext
    internal class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("NWDB")
        {

        }
        //this class needs to supply DbContet within the applicatons's connection string name
        //this name is supplied to DbContext using the constructor of this class



        //we need properties in this class that will be used by
        //  EntityFramework to transport the data into/out of your application
        //each entity will have their own "transportation set" 

        //the coding standard for thsi course will be plural naming for the 
        //  DbSet<T> property name

        public DbSet<Product> Products { get; set; } //references by the BLL controllers.

        public DbSet<Region> Regions { get; set; }

        public DbSet<Region> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

    }
}
