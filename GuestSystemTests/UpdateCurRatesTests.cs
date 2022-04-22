using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace GuestSystemTests
{
    public class UpdateCurRatesTests
    {
        private ApplicationDbContext _db;
        private async Task CreateMocDBWithRates()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);
            var newrates = new CurrentRatesModel();
            newrates.GDPToUSDRate = 1.1;
            newrates.GDPToEuroRate = 1.2;
            newrates.GDPToYenRate = 1.3;
            _db.GBPConversionRates.Add(newrates);

            
            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();

        }
        private async Task CreateMocDBWithRatesAndExpiredDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);
            var newrates = new CurrentRatesModel();
            newrates.GDPToUSDRate = 1.1;
            newrates.GDPToEuroRate = 1.2;
            newrates.GDPToYenRate = 1.3;
            newrates.NextUpdate = DateTime.Now.AddDays(-1);
            _db.GBPConversionRates.Add(newrates);


            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();

        }
        private async Task CreateMocDBWithRatesAndFreshDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);
            var newrates = new CurrentRatesModel();
            newrates.GDPToUSDRate = 1.1;
            newrates.GDPToEuroRate = 1.2;
            newrates.GDPToYenRate = 1.3;
            newrates.NextUpdate = DateTime.Now.AddDays(1);
            _db.GBPConversionRates.Add(newrates);


            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();

        }
        private async Task CreateMocDBNoRates()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new ApplicationDbContext(options);

            
            await _db.SaveChangesAsync();
            _db.Database.EnsureCreated();

        }

        [Test]
        public async Task UpdateFirstTimeLoadAddsRates()
        {
            //Arrange
            await CreateMocDBNoRates();
            //Act
            await UpdateCurRates.Update(_db);
            var rates = _db.GBPConversionRates.FirstOrDefaultAsync().Result;
            //Assert
            if (rates.id != null)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task CheckRatesUpdatesCorrectly()
        {
            //Arrange
            await CreateMocDBWithRates();
            //Act
            await UpdateCurRates.Update(_db);
            var rates = _db.GBPConversionRates.FirstOrDefaultAsync().Result;
            //Assert
            if(rates.GDPToEuroRate == 1.2 && rates.GDPToYenRate == 1.3 && rates.GDPToUSDRate == 1.1)
            {
                Assert.Fail("The Currency Exchange rates did not update");
            }
            if (rates.GDPToEuroRate != 1.2 && rates.GDPToYenRate != 1.3 && rates.GDPToUSDRate != 1.1)
            {
                Assert.Pass();
            }
        }
        [Test]
        public async Task RatesDoesNotUpdateWhenNotExpired()
        {
            //Arrange
            await CreateMocDBWithRatesAndFreshDate();
            var Expected = 1.1;
            //Act
            await UpdateCurRates.CheckRates(_db);
            var Actual = _db.GBPConversionRates.FirstOrDefaultAsync().Result.GDPToUSDRate;
            //assert
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public async Task RatesUpdatesWhenExpired()
        {
            //Arrange
            await CreateMocDBWithRatesAndExpiredDate();
            var Expected = 1.1;
            //Act
            await UpdateCurRates.CheckRates(_db);
            var Actual = _db.GBPConversionRates.FirstOrDefaultAsync().Result.GDPToUSDRate;
            //Assert
            Assert.AreNotEqual(Expected, Actual);
        }
    }
}
