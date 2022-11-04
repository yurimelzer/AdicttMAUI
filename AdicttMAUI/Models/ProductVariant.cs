using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    [Table("product_variant")]
    public class ProductVariant
    {
        [PrimaryKey]
        public long id { get; set; }
        public long produtctId { get; set; }
        public int position { get; set; }
        public double price { get; set; }
        public double promotionalPrice { get; set; }
        public int stock { get; set; }
        public double weight { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double depth { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string gender { get; set; }
        public string barcode { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
