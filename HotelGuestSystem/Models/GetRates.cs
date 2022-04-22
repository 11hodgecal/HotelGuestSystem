using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public static class GetRates
    {
        //converts pounds to dollars
        public static double ToUSD(ApplicationDbContext db, double GBP)
        {
            var rate = db.GBPConversionRates.FirstOrDefaultAsync().Result.GDPToUSDRate;
            return GBP * rate;
        }
        //converts pounds to euros
        public static double ToEuro(ApplicationDbContext db, double GBP)
        {
            var rate = db.GBPConversionRates.FirstOrDefaultAsync().Result.GDPToEuroRate;
            return GBP * rate;
        }
        //converts pounds to yen
        public static double ToYen(ApplicationDbContext db, double GBP)
        {
            var rate = db.GBPConversionRates.FirstOrDefaultAsync().Result.GDPToYenRate;
            return GBP * rate;
        }
        public static List<CustomerServiceViewModel> ServiceRequestViewModelConvert (ApplicationDbContext db, List<RoomServiceModel> unconverted, string currency)
        {
            //creates a list of services that have been converted
            var converted = new List<CustomerServiceViewModel> ();
            foreach (var unconverteditem in unconverted)
            {
                //creates a new room service item
                var roomserviceitem = new CustomerServiceViewModel();

                //returns the converted item details
                roomserviceitem.Id = unconverteditem.Id;
                roomserviceitem.Name = unconverteditem.Name;
                roomserviceitem.ServiceType = unconverteditem.ServiceType;
                roomserviceitem.img = unconverteditem.Imagepath;

                //depending on the users prefered currency a string will be made with there currency
                if (currency == "GBP")
                {
                    roomserviceitem.PreferedCurPrice = $"£{unconverteditem.Price}";
                }
                if (currency == "USD")
                {
                    roomserviceitem.PreferedCurPrice = $"${Math.Round(ToUSD(db,unconverteditem.Price), 2)}";
                }
                if (currency == "EURO")
                {
                    roomserviceitem.PreferedCurPrice = $"€{Math.Round(ToEuro(db, unconverteditem.Price), 2)}";
                }
                if (currency == "Yen")
                {
                    roomserviceitem.PreferedCurPrice = $"¥{Convert.ToInt32(ToYen(db, unconverteditem.Price))}";
                }

                converted.Add(roomserviceitem);

            }
            //returned the converted list
            return converted;
        }
    }
}
