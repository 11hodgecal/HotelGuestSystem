using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelGuestSystem.Controllers
{
    public class CustomerRoomRequestController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _UserManager;

        public CustomerRoomRequestController(ApplicationDbContext db, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _db = db;
            _signInManager = signInManager;
            _UserManager = userManager;
        }
        [Authorize(Roles ="Customer")]
        public IActionResult Index()
        {
            string filter = "RoomService";
            ViewBag.Filter = filter;
            //gets the available services
            var services = _db.RoomServiceItems.Where(s => s.ServiceType == filter).ToListAsync().Result;
            //gets the signed in users prefered currency
            var currency = _signInManager.UserManager.FindByNameAsync(User.Identity.Name).Result.PreferedCurrency;

            //converts the available services to a different currency
            var Viewmodel = GetRates.ServiceRequestViewModelConvert(_db, services, currency);
            return View(Viewmodel);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult Index(string filter)
        {
            ViewBag.Filter = filter;
            //gets the available services
            var services = _db.RoomServiceItems.Where(s => s.ServiceType == filter).ToListAsync().Result;
            //gets the signed in users prefered currency
            var currency = _signInManager.UserManager.FindByNameAsync(User.Identity.Name).Result.PreferedCurrency;

            //converts the available services to a different currency
            var Viewmodel = GetRates.ServiceRequestViewModelConvert(_db, services, currency);
            return View(Viewmodel);
        }
    }
}
