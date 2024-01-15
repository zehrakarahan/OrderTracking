using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? OrderName { get; set; }
        public DateTime? OrderDatetime { get; set; }
        public double? TotalAmount { get; set; }
        public int? OrderStatusId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public virtual OrderStatus? OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
