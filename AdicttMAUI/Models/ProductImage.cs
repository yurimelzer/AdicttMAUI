using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    public class ProductImage
    {
        public long id { get; set; }
        public long productId { get; set; }
        public string source { get; set; }
        public int position { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
