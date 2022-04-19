using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Sname { get; set; }
    }
}
