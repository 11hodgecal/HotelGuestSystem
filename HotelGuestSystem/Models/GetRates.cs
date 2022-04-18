using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
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
    }
}
