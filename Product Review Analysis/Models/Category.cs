using System;
using System.Collections.Generic;

namespace Product_Review_Analysis.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
