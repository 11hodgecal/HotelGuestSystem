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
    public class ExpiredAccountDeleteTests
    {
        private ApplicationDbContext _db;
        private async Task CreateMocDBAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);

            UserModel user = new UserModel();
            user.Id = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            user.UserName = "Expired@Test.com";
            user.NormalizedUserName = "Expired@Test.com".ToUpper();
            user.NormalizedEmail = "Expired@Test.com".ToUpper();
            user.Email = "Expired@Test.com";
            user.Fname = "Bob";
            user.Sname = "Nobody";
            user.LockoutEnabled = false;
            user.ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29";
            user.PreferedCurrency = "Yen";
            user.Bookingid = 1;

            await _db.Users.AddAsync(user);

            BookingModel bookingModel = new BookingModel();
            bookingModel.Id = 1;
            bookingModel.Email = "Expired@test.com";
            bookingModel.Isactivated = true;
            bookingModel.BookingStart = DateTime.Now.AddDays(-5);
            bookingModel.BookingEnd = DateTime.Now.AddDays(-1);
            await _db.Bookings.AddAsync(bookingModel);


            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();

        }
        [Test]
        public async Task ExpiredAccountDeleted()
        {
            //Arrange
            await CreateMocDBAsync();
            //Act
            await AccountExpiredChecker.DeleteExpired(_db);
            var account = _db.Users.Where(s => s.UserName == "Expired@Test.com").FirstOrDefaultAsync().Result;
            //Assert
            Assert.IsNull(account);
            
        }
    }
}
