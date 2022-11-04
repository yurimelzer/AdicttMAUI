using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    [Table("category")]
    public class Category
    {
        [PrimaryKey]
        public long id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public long parent { get; set; }

        [ManyToMany(typeof(ProductCategory), CascadeOperations = CascadeOperation.All)]
        public List<Product> products { get; set; }
    }
}
