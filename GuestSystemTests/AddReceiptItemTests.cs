using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestSystemTests
{
    public class AddReceiptItemTests
    {
        private ApplicationDbContext _db;
        private async Task CreateMocDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);

            UserModel user = new UserModel();
            user.Id = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            user.UserName = "Test@Test.com";
            user.NormalizedUserName = "Test@Test.com".ToUpper();
            user.NormalizedEmail = "Test@Test.com".ToUpper();
            user.Email = "Test@Test.com";
            user.Fname = "Bob";
            user.Sname = "Nobody";
            user.LockoutEnabled = false;
            user.ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29";
            user.PreferedCurrency = "Yen";
            _db.Users.Add(user);

            UserModel user2 = new UserModel();
            user.Id = "27b9df24-a133-43e2-8dd2-aef04ddb2b8c";
            user.UserName = "Test2@Test.com";
            user.NormalizedUserName = "Test2@Test.com".ToUpper();
            user.NormalizedEmail = "Test2@Test.com".ToUpper();
            user.Email = "Test2@Test.com";
            user.Fname = "Bob2";
            user.Sname = "Nobody2";
            user.LockoutEnabled = false;
            user.ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29";
            user.PreferedCurrency = "Yen";
            _db.Users.Add(user2);

            var service = new RoomServiceModel()
            {
                Id = 3,
                Name = "Change Sheets",
                Price = 1,
                ServiceType = "RoomService"
            };
            _db.RoomServiceItems.Add(service);

            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();
        }
        [Test]
        public async Task FirstTimeAddReceiptItem()
        {
            //Arrange
            await CreateMocDB();
            var request = new RequestModel();
            request.Id = 1;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            _db.request.Add(request);
            await _db.SaveChangesAsync();

            //Act
            await UpdateReceipt.Additem(request, _db);
            var item = _db.BillItems.FirstOrDefaultAsync().Result;

            //Assert
            Assert.NotNull(item);

        }
        [Test]
        public async Task ANewUserCanHaveABillingItemOfTheSameName()
        {
            //Arrange
            await CreateMocDB();
            var billingitem = new BillItem();
            billingitem.Id = 1;
            billingitem.ItemName = "Change Sheets";
            billingitem.Price = 0;
            billingitem.Quantity = 1;
            billingitem.CustomerId = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            _db.BillItems.Add(billingitem);
            await _db.SaveChangesAsync();

            var request = new RequestModel();
            request.Id = 1;
            request.CustomerID = "27b9df24-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            _db.request.Add(request);
            await _db.SaveChangesAsync();

            //Act
            await UpdateReceipt.Additem(request, _db);
            var item = _db.BillItems.Where(s=>s.CustomerId == "27b9df24-a133-43e2-8dd2-aef04ddb2b8c").FirstOrDefaultAsync().Result;

            //Assert
            Assert.NotNull(item);

        }
        [Test]
        public async Task AddQuantityIfBillitemExists()
        {
            //Arrange
            await CreateMocDB();
            var request = new RequestModel();
            request.Id = 1;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            var request2 = new RequestModel();
            request.Id = 2;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            _db.request.Add(request);
            await _db.SaveChangesAsync();
            var expected = 2;
            //Act
            await UpdateReceipt.Additem(request, _db);
            await UpdateReceipt.Additem(request, _db);
            var item = _db.BillItems.FirstOrDefaultAsync().Result;

            //Assert
            Assert.AreEqual(expected, item.Quantity);

        }
        [Test]
        public async Task PriceUpdatesIfBillitemExists()
        {
            //Arrange
            await CreateMocDB();
            var request = new RequestModel();
            request.Id = 1;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            var request2 = new RequestModel();
            request.Id = 2;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            _db.request.Add(request);
            await _db.SaveChangesAsync();
            var expected = 2;
            //Act
            await UpdateReceipt.Additem(request, _db);
            await UpdateReceipt.Additem(request, _db);
            var item = _db.BillItems.FirstOrDefaultAsync().Result;

            //Assert
            Assert.AreEqual(expected, item.Price);

        }
    }
}
