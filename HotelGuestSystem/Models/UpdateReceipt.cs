using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public static class UpdateReceipt
    {
        private static async Task AddOneToQuantity(RequestModel Request, ApplicationDbContext db)
        {
            //gets the attached items name
            var Item = await db.RoomServiceItems.Where(s => s.Id == Request.ItemID).FirstOrDefaultAsync();

            //gets the existing billing item and changes the quantity
            var billitems = await db.BillItems.Where(s=>s.CustomerId == Request.CustomerID).ToListAsync();
            foreach(var billitem in billitems)
            {
                if(billitem.ItemName == Item.Name)
                {
                    billitem.Price += Item.Price;
                    billitem.Quantity += 1;
                }
            }
            //saves
            await db.SaveChangesAsync();
        }
        private static async Task AddNewItem(RequestModel Request, ApplicationDbContext db)
        {
            //gets the item attached to the request
            var Item = db.RoomServiceItems.Where(s => s.Id == Request.ItemID).FirstOrDefaultAsync().Result;

            //creates the new billing item
            BillItem billItem = new BillItem()
            {
                CustomerId = Request.CustomerID,
                ItemName = Item.Name,
                Price = Item.Price,
                Type = Item.ServiceType,
                Quantity = 1,
            };
            //adds the new billing item
            db.BillItems.Add(billItem);
            await db.SaveChangesAsync();
        }
        public static async Task Additem(RequestModel Request, ApplicationDbContext db)
        {
            //gets the item
            var ItemName = db.RoomServiceItems.Where(s=> s.Id == Request.ItemID).FirstOrDefaultAsync().Result.Name;
            //gets the existing billing items items
            var billingitems = await db.BillItems.ToListAsync();
            //bool for measuring whether the billing item already exists
            bool Exists = false;

            //if the current customer already has the item in there will say it exists
            foreach(var billingitem in billingitems)
            {
                if(billingitem.ItemName == ItemName && Request.CustomerID == billingitem.CustomerId)
                {
                    Exists = true;
                }
            }

            //if it exists add one to the quantity
            if (Exists)
            {
                await AddOneToQuantity(Request, db);
            }
            //if the billitem does not exist for the current user create it
            if (!Exists)
            {
                await AddNewItem(Request, db);
            }


        }
    }
}
