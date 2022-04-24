using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class RequestModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerID { get; set; }
        [Required]
        public int ItemID{ get; set; }
        public DateTime requestmade {get; set;}
        [DefaultValue(false)]
        public bool Delivered { get; set; }
    }
}
