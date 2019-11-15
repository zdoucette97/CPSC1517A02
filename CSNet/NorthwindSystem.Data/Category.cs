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
    [Table("Category")]
    public class Category
    {
        [Key] //6 and 7 using a view with no table and no key. Looks like an entity but is not one. No table, no key, no dbset forCurrentOfferings it is a view)
        //just create a classs with the. we are filling the view with the ProductController code
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public string PictureMimeType { get; set; } //picture file type (jpg, png, etc)

    }
}
