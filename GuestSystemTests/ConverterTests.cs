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
    public class ConverterTests
    {
        private ApplicationDbContext _db;
        private async Task CreateMocDBAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);
            var newrates = new CurrentRatesModel();

            newrates.GDPToUSDRate = 1.1;
            newrates.GDPToEuroRate = 1.2;
            newrates.GDPToYenRate = 1.3;
            _db.GBPConversionRates.Add(newrates);

            var newservice = new RoomServiceModel()
            {
                Id = 1,
                Name = "test",
                Price = 1,
            };
            _db.RoomServiceItems.Add(newservice);
            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();
        }

        [Test]
        public async Task ToUSDWorksCorrectly()
        {
            //arrange
            await CreateMocDBAsync();
            var Rates = await _db.GBPConversionRates.FirstOrDefaultAsync();
            var USDRates = Rates.GDPToUSDRate;
            double expected = 40 * USDRates;
            //act
            var actual = GetRates.ToUSD(_db, 40);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ToYenWorksCorrectly()
        {
            //arrange
            await CreateMocDBAsync();
            var Rates = await _db.GBPConversionRates.FirstOrDefaultAsync();
            var YenRates = Rates.GDPToYenRate;
            double expected = 40 * YenRates;
            //act
            var actual = GetRates.ToYen(_db, 40);

            //assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task ToEuroWorksCorrectly()
        {
            //arrange
            await CreateMocDBAsync();
            var Rates = await _db.GBPConversionRates.FirstOrDefaultAsync();
            var EuroRates = Rates.GDPToEuroRate;
            double expected = 40 * EuroRates;
            //act
            var actual = GetRates.ToEuro(_db, 40);

            //assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task ToPoundServiceViewWorks()
        {
            //arrange
            await CreateMocDBAsync();
            var RoomServiceItems = await _db.RoomServiceItems.ToListAsync();
            string Currency = "GBP";
            string expected = "£1";
            //Act
            var Actual = GetRates
                .ServiceRequestViewModelConvert(_db, RoomServiceItems, Currency)[0]
                .PreferedCurPrice;

            //Assert
            Assert.AreEqual(expected, Actual);

        }
        [Test]
        public async Task ToUSDServiceViewWorks()
        {
            //arrange
            await CreateMocDBAsync();
            var RoomServiceItems = await _db.RoomServiceItems.ToListAsync();
            string Currency = "USD";
            string expected = "$1.1";
            //Act
            var Actual = GetRates.ServiceRequestViewModelConvert
                (_db, RoomServiceItems, Currency)[0].PreferedCurPrice;

            //Assert
            Assert.AreEqual(expected, Actual);

        }
        [Test]
        public async Task ToEuroServiceViewWorks()
        {
            //arrange
            await CreateMocDBAsync();
            var RoomServiceItems = await _db.RoomServiceItems.ToListAsync();
            string Currency = "EURO";
            string expected = "€1.2";
            //Act
            var Actual = GetRates.ServiceRequestViewModelConvert
                (_db, RoomServiceItems, Currency)[0].PreferedCurPrice;

            //Assert
            Assert.AreEqual(expected, Actual);

        }
        [Test]
        public async Task ToYenServiceViewWorks()
        {
            //arrange
            await CreateMocDBAsync();
            var RoomServiceItems = await _db.RoomServiceItems.ToListAsync();
            string Currency = "Yen";
            string expected = "¥1";
            //Act
            var Actual = GetRates.ServiceRequestViewModelConvert
                (_db, RoomServiceItems, Currency)[0].PreferedCurPrice;

            //Assert
            Assert.AreEqual(expected, Actual);

        }


    }
}
