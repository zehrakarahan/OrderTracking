using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public DateTime? OrderCreateDatetime { get; set; }
        public DateTime? EstimatedDeliveryDatetime { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? OrderQuantity { get; set; }
        public int? DeliveryNumber { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
