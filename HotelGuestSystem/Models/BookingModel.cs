using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added the booking model from the booking program
namespace HotelGuestSystem.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }
        public string CustomerFName { get; set; }
        public string CustomerSName { get; set; }
        public string Email { get; set; } 
        public string BookingCode { get; set; }
        public DateTime BookingStart{ get; set; } 
        public DateTime BookingEnd{ get; set; }
        [DefaultValue(false)]
        public bool Isactivated { get; set; }

    }
}
