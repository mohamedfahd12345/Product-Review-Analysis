using System;
using System.Collections.Generic;

namespace Product_Review_Analysis.Models
{
    public partial class Product
    {
        public Product()
        {
            Compares = new HashSet<Compare>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Photo { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Compare> Compares { get; set; }
    }
}
