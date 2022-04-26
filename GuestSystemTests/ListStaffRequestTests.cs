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
    public class ListStaffRequestTests
    {
        private ApplicationDbContext _db;
        
        private async Task CreateMocDBForCreateView()
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

            var service = new RoomServiceModel()
            {
                Id = 3,
                Name = "Change Sheets",
                Price = 0,
                ServiceType = "RoomService"
            };
            _db.RoomServiceItems.Add(service);

            var request = new RequestModel();
            request.Id = 1;
            request.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request.ItemID = 3;
            request.Delivered = false;

            var request2 = new RequestModel();
            request2.Id = 2;
            request2.CustomerID = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            request2.ItemID = 3;
            request2.Delivered = true;

            _db.request.Add(request);
            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();
        }

        [Test]
        public async Task CompleteRequest()
        {
            //arrange
            await CreateMocDBForCreateView();
            //act
            await ListStaffRequests.CompleteRequest(1, _db);
            var result = _db.request.FirstOrDefault();
            //assert
            Assert.IsTrue(result.Delivered);
        }

        [Test]
        public async Task CreateViewReturnsOnlyActiveRequests()
        {
            //arrange
            await CreateMocDBForCreateView();
            //act
            var requests = await _db.request.ToListAsync();
            var RequestViews = ListStaffRequests.CreateView(requests,_db);
            var InactiveRequestPresent = false;
            foreach(var requestView in RequestViews)
            {
                if(requestView.IsDelivered == true)
                {
                    InactiveRequestPresent = true;
                }
            }
            //assert
            Assert.IsFalse(InactiveRequestPresent);
        }
    }
}
