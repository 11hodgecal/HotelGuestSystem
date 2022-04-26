using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public static class Bill
    {
        //gets the current customers bill
        public static async Task<List<BillItem>> GetBill(string CustomerID, ApplicationDbContext db)
        {
            var CustomerBillItems = await db.BillItems.Where(s => s.CustomerId == CustomerID).ToListAsync();
            return CustomerBillItems;
        }
        public static async Task<string> Total(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = await GetBill(CustomerID, db);
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
        public static async Task<string> FoodTotal(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = await GetBill(CustomerID, db);
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
        public static async Task<string> DrinkTotal(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = await GetBill(CustomerID, db);
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
        public static async Task<string> RoomServiceTotal(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = GetBill(CustomerID, db).Result.Where(s=> s.Type == "RoomService").ToList();
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Room Service Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Room Service Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Room Service Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Room Service Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
    }
}
