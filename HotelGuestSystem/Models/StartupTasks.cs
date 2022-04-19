using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public class StartupTasks : IStartupTasks
    {
        public async Task Execute()
        {
            //Checks the Currency rates
            await UpdateCurRates.CheckRates();
        }
    }
}
