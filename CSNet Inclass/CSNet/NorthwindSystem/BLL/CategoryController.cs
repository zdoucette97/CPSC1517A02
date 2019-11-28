using System;
using System.Collections.Generic;
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
    [DataObject] //exposes the controller to the ObjectDataSource wizard
    public class CategoryController
    {
        //expose a specific method to the ODS
        [DataObjectMethod(DataObjectMethodType.Select, false)] //if true, serves as a default method and is picked by the wizard by default. not necessary and can be very annoying
        public List<Category> Categories_List()
        {
            using(var context = new NorthwindContext())
            {
                return context.Categories.ToList();
            }
        }
    }
}
