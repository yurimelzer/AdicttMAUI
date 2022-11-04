using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    [Table("product")]
    public class Product
    {
        [PrimaryKey]
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public string specification { get; set; }
        public string brand { get; set; }
        public bool freeShipping { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public string tags { get; set; }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return this.id.Equals(product.id);
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(this.id / 10);
        }
    }
}
