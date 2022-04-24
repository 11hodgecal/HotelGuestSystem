using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Request(int ItemID)
        {
            if (ItemID == null)
            {
                return RedirectToAction("Index");
            }

            //gets the chosen item
            var ChosenItem = await _db.RoomServiceItems.Where(s => s.Id == ItemID).FirstOrDefaultAsync();

            //puts the items name and id in the viewbag
            ViewBag.ItemID = ChosenItem.Id;
            ViewBag.ItemName = ChosenItem.Name;
            
            //change the view
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Request(RequestModel request)
        {
            //Assigns the request to the current user 
            request.CustomerID = _UserManager.FindByNameAsync(User.Identity.Name).Result.Id;
            //Adds when the request was made
            request.requestmade = DateTime.Now;
            //Adds the request to the database
            await _db.request.AddAsync(request);
            //saves the changes and gives the view
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
