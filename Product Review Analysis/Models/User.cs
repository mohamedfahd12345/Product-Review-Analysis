using System;
using System.Collections.Generic;

namespace Product_Review_Analysis.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Compares = new HashSet<Compare>();
            Rateings = new HashSet<Rateing>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }

        public virtual AspNetUser IdNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Compare> Compares { get; set; }
        public virtual ICollection<Rateing> Rateings { get; set; }
    }
}
