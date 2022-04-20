using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class RoomServiceModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ServiceTypes ServiceType { get; set; }
        [DefaultValue(0)]
        public double Price { get; set; }

        public DateTime Due { get; set; }
        
        public string Imagepath { get; set; }
    }
}
