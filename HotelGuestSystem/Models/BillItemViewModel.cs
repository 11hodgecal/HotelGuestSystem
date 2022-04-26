using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class BillItemViewModel
    {
        public string Type { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
    }
}
