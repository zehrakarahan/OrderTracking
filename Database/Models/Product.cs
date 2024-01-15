using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Stoks = new HashSet<Stok>();
        }

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public DateTime? ProductCreateDatetime { get; set; }
        public int? ProductUnitId { get; set; }
        public int? ProductDisCountRate { get; set; }

        public virtual Unit? ProductUnit { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Stok> Stoks { get; set; }
    }
}
