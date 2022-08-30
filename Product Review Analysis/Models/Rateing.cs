using System;
using System.Collections.Generic;

namespace Product_Review_Analysis.Models
{
    public partial class Rateing
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? ProductId { get; set; }
        public double? Rate { get; set; }

        public virtual User? User { get; set; }
    }
}
