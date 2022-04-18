namespace HotelGuestSystem.Models
{
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium;
    using System;
    using HotelGuestSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public static class UpdateCurRates
    {

        public static async Task Update(ApplicationDbContext Context)
        {
            //scrapes the currency converstion rates of a website
            var driver = new ChromeDriver("C:\\Program Files\\Google\\Chrome\\Application");
            driver.Navigate().GoToUrl("https://www.exchangerates.org.uk/");

            string GDPToUsd = driver.FindElement(By.XPath("/html/body/div[2]/div[7]/div[1]/div[1]/div[1]/div[1]/div[2]/div/div[1]/table/tbody/tr[2]/td[4]/strong")).Text;
            string GBPToEUR = driver.FindElement(By.XPath("/html/body/div[2]/div[7]/div[1]/div[1]/div[1]/div[1]/div[2]/div/div[1]/table/tbody/tr[1]/td[4]/strong")).Text;
            string GBPToYen = driver.FindElement(By.XPath("/html/body/div[2]/div[7]/div[1]/div[1]/div[1]/div[1]/div[2]/div/div[1]/table/tbody/tr[6]/td[4]/strong")).Text;
            double GDPToUsdRate = Convert.ToDouble(GDPToUsd);
            double GBPToEURRate = Convert.ToDouble(GBPToEUR);
            double GBPToYenRate = Convert.ToDouble(GBPToYen);
            driver.Close();
            
            
            //gets the current rates
            var rates = await Context.GBPConversionRates.ToListAsync();
            //if the rates dont exist create them 
            if (rates.Count == 0)
            {
                var newRates = new CurrentRatesModel();

                newRates.GDPToUSDRate = GDPToUsdRate;
                newRates.GDPToYenRate = GBPToYenRate;
                newRates.GDPToEuroRate = GBPToEURRate;

                newRates.NextUpdate = DateTime.Now.AddDays(1);
                Context.GBPConversionRates.Add(newRates);
                await Context.SaveChangesAsync();
            }
            //if the currency converstion rates exist update them with newer ones
            if (rates.Count == 1)
            {
                var newRates = Context.GBPConversionRates.FirstOrDefaultAsync().Result;
                newRates.GDPToUSDRate = GDPToUsdRate;
                newRates.GDPToYenRate = GBPToYenRate;
                newRates.GDPToEuroRate = GBPToEURRate;

                newRates.NextUpdate = DateTime.Now.AddDays(1);
                Context.GBPConversionRates.Update(newRates);
                await Context.SaveChangesAsync();

            }
            
        }
        //manual test
        public static async Task CheckRates()
        {

            //initalizes the database
            ApplicationDbContext _db;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseSqlite("DataSource=C:\\Users\\callu\\Desktop\\HotelGuestSystem\\app.db").Options;
            _db = new ApplicationDbContext(options);
            await _db.SaveChangesAsync();

            
            using (_db)
            {
                //gets the rates
                var rates = await _db.GBPConversionRates.ToListAsync();
                //if they dont exist update them
                if(rates.Count == 0)
                {
                    await Update(_db);
                }

                //if the currency rates are set to update update them
                var needupdate = DateTime.Compare(DateTime.Now, rates[0].NextUpdate);
                if (needupdate == 1)
                {
                    await Update(_db);
                }
            }
            
        }

    }
}
