using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            Orders = new HashSet<Order>();
            Stoks = new HashSet<Stok>();
        }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Stok> Stoks { get; set; }
    }
}
