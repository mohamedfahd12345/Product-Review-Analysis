using System;
using System.Collections.Generic;

namespace Product_Review_Analysis.Models
{
    public partial class Compare
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? UserId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
