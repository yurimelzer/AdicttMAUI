using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    public class ProductCategory
    {
        [ForeignKey(typeof(Product))]
        public long productId { get; set; }

        [ForeignKey(typeof(Category))]
        public long categoryId { get; set; }

        public override bool Equals(object obj)
        {
            var productCategory = obj as ProductCategory;
            string thisEquals = this.productId.ToString() + this.categoryId.ToString();
            string objEquals = productCategory.productId.ToString() + this.categoryId.ToString();
            return thisEquals.Equals(objEquals);
        }
    }
}
