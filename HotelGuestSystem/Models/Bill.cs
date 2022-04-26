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
        //converts the bill items to a format that can be displayed as well as in the customers prefered currency
        public static async Task<List<BillItemViewModel>> ConvertBill(string CustomerID, ApplicationDbContext db,List<BillItem> billist, string PreferedCurrency)
        {
            List<BillItemViewModel> Converted = new List<BillItemViewModel>();

            foreach(var billItem in billist)
            {
                BillItemViewModel ConvertedItem = new BillItemViewModel();

                ConvertedItem.Type = billItem.Type;
                ConvertedItem.ItemName = billItem.ItemName;
                ConvertedItem.Quantity = billItem.Quantity;

                //depending on the users prefered currency a string will be made with there currency
                if (PreferedCurrency == "GBP")
                {
                    ConvertedItem.Price = $"£{billItem.Price}";
                }
                if (PreferedCurrency == "USD")
                {
                    ConvertedItem.Price = $"${Math.Round(GetRates.ToUSD(db, billItem.Price), 2)}";
                }
                if (PreferedCurrency == "EURO")
                {
                    ConvertedItem.Price = $"€{Math.Round(GetRates.ToEuro(db, billItem.Price), 2)}";
                }
                if (PreferedCurrency == "Yen")
                {
                    ConvertedItem.Price = $"¥{Convert.ToInt32(GetRates.ToYen(db, billItem.Price))}";
                }
                Converted.Add(ConvertedItem);
            }

            return Converted;
        }
        //gets the total
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
        //gets the food total
        public static async Task<string> FoodTotal(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = GetBill(CustomerID, db).Result.Where(s => s.Type == "Food").ToList();
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Food Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Food Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Food Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Food Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
        //gets the drink total
        public static async Task<string> DrinkTotal(string CustomerID, ApplicationDbContext db, string PreferedCurrency)
        {
            double total = 0;
            var items = GetBill(CustomerID, db).Result.Where(s => s.Type == "Drink").ToList();
            string TotalConverted;
            foreach (var item in items)
            {
                total += item.Price;
            }

            //depending on the users prefered currency a string will be made with there currency
            if (PreferedCurrency == "GBP")
            {
                TotalConverted = $"Drink Total: £{total}";
                return TotalConverted;
            }
            if (PreferedCurrency == "USD")
            {
                TotalConverted = $"Drink Total: ${Math.Round(GetRates.ToUSD(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "EURO")
            {
                TotalConverted = $"Drink Total: €{Math.Round(GetRates.ToEuro(db, total), 2)}";
                return TotalConverted;
            }
            if (PreferedCurrency == "Yen")
            {
                TotalConverted = $"Drink Total: ¥{Convert.ToInt32(GetRates.ToYen(db, total))}";
                return TotalConverted;
            }
            return "Not Available";

        }
        //gets the room service total
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
