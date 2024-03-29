﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Models
{
    public class ProductImage
    {
        [PrimaryKey]
        public long id { get; set; }

        [ForeignKey(typeof(Product))]
        public long productId { get; set; }
        public string imageSource { get; set; }
        public int position { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
