using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelGuestSystem.Controllers
{
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserModel> _userManager;

        public BillController(ApplicationDbContext db, UserManager<UserModel> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            var CurrentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;

            //gets the overall totals and the totals for each catagory
            ViewBag.Total = Bill.Total(CurrentUser.Id,_db,CurrentUser.PreferedCurrency).Result;
            ViewBag.FoodTotal = Bill.FoodTotal(CurrentUser.Id, _db, CurrentUser.PreferedCurrency).Result;
            ViewBag.DrinkTotal = Bill.DrinkTotal(CurrentUser.Id, _db,CurrentUser.PreferedCurrency).Result;
            ViewBag.RoomServiceTotal = Bill.RoomServiceTotal(CurrentUser.Id, _db, CurrentUser.PreferedCurrency).Result;

            //converts the items to the prefeed currency
            var Billitems = Bill.GetBill(CurrentUser.Id, _db).Result;
            var ConvertedBillItems = Bill.ConvertBill(CurrentUser.Id, _db,Billitems,CurrentUser.PreferedCurrency).Result;
            return View(ConvertedBillItems);
        }
    }
}
