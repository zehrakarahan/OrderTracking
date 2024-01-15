using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public partial class Stok
    {
        public int Id { get; set; }
        public int? StokPiece { get; set; }
        public int? ProductId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public virtual Product? Product { get; set; }
    }
}
