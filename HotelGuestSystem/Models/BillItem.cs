using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class BillItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public string Type { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
