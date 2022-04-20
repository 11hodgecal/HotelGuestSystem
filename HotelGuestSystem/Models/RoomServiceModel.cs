using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelGuestSystem.Models
{
    public class RoomServiceModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ServiceType { get; set; }
        [DefaultValue(0)]
        public double Price { get; set; }
        
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile img { get; set; }
    }
}
