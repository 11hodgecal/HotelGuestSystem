using System;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestSystem.Models
{
    public class CurrentRatesModel
    {
        [Key]
        public int id { get; set; }

        //pound to US dollar
        public double GDPToUSDRate { get; set; }
        //Pound to Euro
        public double GDPToEuroRate { get; set; }
        //Pound to Yen
        public double GDPToYenRate { get; set; }
        //The next available update
        public DateTime NextUpdate { get; set; }
    }
}
