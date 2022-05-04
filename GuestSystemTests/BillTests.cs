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
    public class BillTests
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

            var billingitem = new BillItem();
            billingitem.Id = 1;
            billingitem.ItemName = "Change Sheets";
            billingitem.Price = 1;
            billingitem.Quantity = 1;
            billingitem.CustomerId = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            billingitem.Type = "Food";
            _db.BillItems.Add(billingitem);

            var billingitem2 = new BillItem();
            billingitem2.Id = 2;
            billingitem2.ItemName = "Toilet Roll";
            billingitem2.Price = 2;
            billingitem2.Quantity = 2;
            billingitem2.CustomerId = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            billingitem2.Type = "Drink";
            _db.BillItems.Add(billingitem2);

            var billingitem3 = new BillItem();
            billingitem3.Id = 3;
            billingitem3.ItemName = "Toilet Roll";
            billingitem3.Price = 2;
            billingitem3.Quantity = 2;
            billingitem3.CustomerId = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            billingitem3.Type = "RoomService";
            _db.BillItems.Add(billingitem3);

            var billingitem4 = new BillItem();
            billingitem4.Id = 4;
            billingitem4.ItemName = "Change Sheets";
            billingitem4.Price = 3;
            billingitem4.Quantity = 1;
            billingitem4.CustomerId = "27b9df24-a133-43e2-8dd2-aef04ddb2b8c";
            billingitem4.Type = "RoomService";
            _db.BillItems.Add(billingitem);

            var newrates = new CurrentRatesModel();
            newrates.GDPToUSDRate = 1.1;
            newrates.GDPToEuroRate = 1.2;
            newrates.GDPToYenRate = 1.3;
            _db.GBPConversionRates.Add(newrates);

            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();
        }

        [Test]
        public async Task ReturnsOnlyTheCurrentUsersItems()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var OnlyCustomersItems = true;
            //Act
            var CustomerBill = Bill.GetBill("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db);

            foreach (var item in billitems)
            {
                if(item.CustomerId != "27b9df34-a133-43e2-8dd2-aef04ddb2b8c")
                {
                    OnlyCustomersItems = false;
                }
            }
            //Assert
            
            Assert.That(OnlyCustomersItems, Is.True);
        }
        [Test]
        public async Task TotalCorrectGBP()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Total: £5";
            //Act
            var CustomerBillTotal = 
               Bill.Total("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db,"GBP").Result;
            //Assert
            
            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task TotalCorrectUSD()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Total: $5.5";
            //Act
            var CustomerBillTotal = 
                Bill.Total("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "USD").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task TotalCorrectEuro()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Total: €6";
            //Act
            var CustomerBillTotal = 
                Bill.Total("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "EURO").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task TotalCorrectYen()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Total: ¥6";
            //Act
            var CustomerBillTotal = 
                Bill.Total("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "Yen").Result;
            //Assert
            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task FoodTotalCorrectGBP()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Food Total: £1";
            //Act
            var CustomerBillTotal = 
                Bill.FoodTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "GBP").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task FoodTotalCorrectUSD()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Food Total: $1.1";
            //Act
            var CustomerBillTotal = 
                Bill.FoodTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "USD").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task FoodTotalCorrectEuro()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Food Total: €1.2";
            //Act
            var CustomerBillTotal = 
                Bill.FoodTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "EURO").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task FoodTotalCorrectYen()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Food Total: ¥1";
            //Act
            var CustomerBillTotal = 
                Bill.FoodTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "Yen").Result;
            //Assert
            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task DrinkTotalCorrectGBP()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Drink Total: £2";
            //Act
            var CustomerBillTotal = 
                Bill.DrinkTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "GBP").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task DrinkTotalCorrectUSD()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Drink Total: $2.2";
            //Act
            var CustomerBillTotal = 
                Bill.DrinkTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "USD").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task DrinkTotalCorrectEuro()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Drink Total: €2.4";
            //Act
            var CustomerBillTotal = 
                Bill.DrinkTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "EURO").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task DrinkTotalCorrectYen()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Drink Total: ¥3";
            //Act
            var CustomerBillTotal = 
                Bill.DrinkTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "Yen").Result;
            //Assert
            Assert.AreEqual(Expected, CustomerBillTotal);
        }

        [Test]
        public async Task RoomServiceTotalCorrectGBP()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Room Service Total: £2";
            //Act
            var CustomerBillTotal = 
                Bill.RoomServiceTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "GBP").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task RoomServiceTotalCorrectUSD()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Room Service Total: $2.2";
            //Act
            var CustomerBillTotal = 
                Bill.RoomServiceTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "USD").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task RoomServiceTotalCorrectEuro()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Room Service Total: €2.4";
            //Act
            var CustomerBillTotal = 
                Bill.RoomServiceTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "EURO").Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBillTotal);
        }
        [Test]
        public async Task RoomServiceTotalCorrectYen()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "Room Service Total: ¥3";
            //Act
            var CustomerBillTotal = 
                Bill.RoomServiceTotal("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, "Yen").Result;
            //Assert
            Assert.AreEqual(Expected, CustomerBillTotal);
        }


        [Test]
        public async Task RoomServiceListItemsConvertCorrectlyGBP()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "£1";
            //Act
            var CustomerBill = 
                Bill.ConvertBill("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, billitems, "GBP")
                .Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBill[0].Price);
        }
        [Test]
        public async Task RoomServiceListItemsConvertCorrectlyUSD()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "$1.1";
            //Act
            var CustomerBill = 
                Bill.ConvertBill("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db,billitems,"USD")
                .Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBill[0].Price);
        }
        [Test]
        public async Task RoomServiceListItemsConvertCorrectlyEuro()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "€1.2";
            //Act
            var CustomerBill = 
                Bill.ConvertBill("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, billitems, "EURO")
                .Result;
            //Assert

            Assert.AreEqual(Expected, CustomerBill[0].Price);
        }
        [Test]
        public async Task RoomServiceListItemsConvertCorrectlyYen()
        {
            //Arrange
            await CreateMocDB();
            var billitems = await _db.BillItems.ToListAsync();
            var Expected = "¥1";
            //Act
            var CustomerBill = 
                Bill.ConvertBill("27b9df34-a133-43e2-8dd2-aef04ddb2b8c", _db, billitems, "Yen")
                .Result;
            //Assert
            Assert.AreEqual(Expected, CustomerBill[0].Price);
        }

    }
}
