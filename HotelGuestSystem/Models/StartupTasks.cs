using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public class StartupTasks : IStartupTasks
    {
        public async Task Execute()
        {
            //initalizes the database
            ApplicationDbContext _db;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseSqlite("DataSource=C:\\Users\\callu\\Desktop\\HotelGuestSystem\\app.db").Options;
            _db = new ApplicationDbContext(options);
            _db.Database.EnsureCreated();
            await _db.SaveChangesAsync();


            using (_db)
            {
                //Deletes Expired Customer accounts
                await AccountExpiredChecker.DeleteExpired(_db);
                //Checks the Currency rates
                await UpdateCurRates.CheckRates(_db);
            }
        }
    }
}
