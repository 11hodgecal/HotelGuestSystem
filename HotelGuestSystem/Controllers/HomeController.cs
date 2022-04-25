using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelGuestSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //redirects the admin to the user manager on log in
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "User");
                }
                //redirects the customer to the request manager on log in 
                if (User.IsInRole("Customer"))
                {
                    return RedirectToAction("Index", "CustomerRoomRequest");
                }
                //redirects the Staff to the customer requests on log in 
                if (User.IsInRole("Staff"))
                {
                    return RedirectToAction("Index", "StaffRespond");
                }
            }
            
            return View();
        }
        //allows a Guest to check into the system with a booking code
        [HttpPost]
        public async Task<IActionResult> Index(CheckinViewModel Checkin)
        {
            //checks whether the booking code is present and gives an error
            if (Checkin.code == null)
            {
                ViewBag.error = "Please enter a booking code";
                return View();
            }
            //checks whether the booking code has a correct ammount of characters and gives an error
            if (Checkin.code.Length != 10)
            {
                ViewBag.error = "Please enter a 10 character long booking code";
                return View();
            }
            //gets all the current bookings
            var bookings = await _db.Bookings.ToListAsync();
            foreach (var booking in bookings)
            {
                //finds the specific booking matching the user input
                if(booking.BookingCode == Checkin.code)
                {
                    //send the user an error if they are not expected
                    var cancheckin = DateTime.Compare(DateTime.Now, booking.BookingStart);
                    if (cancheckin == -1)
                    {
                        ViewBag.error = $"We are not expecting you until {booking.BookingStart.Date.ToString("D")}";
                        return View();
                    }
                    //send the user an error if there already checked in
                    if (booking.Isactivated == true)
                    {
                        ViewBag.error = $"You have already checked in";
                        return View();
                    }
                    //tell the user there booking has expired if they missed there holiday
                    var expired = DateTime.Compare(DateTime.Now, booking.BookingEnd);
                    if (expired == 1 && booking.Isactivated == false)
                    {
                        ViewBag.error = $"You have missed your holiday";
                        return View();
                    }

                    //creates a new user that can sign in
                    UserModel NewGuest = new UserModel();
                    NewGuest.Fname = booking.CustomerFName;
                    NewGuest.Email = booking.Email;
                    NewGuest.Sname = booking.CustomerSName;
                    NewGuest.UserName = booking.Email;
                    NewGuest.PreferedCurrency = Checkin.currency;
                    //attach the booking to the user
                    NewGuest.Bookingid = booking.Id;
                    
                    //creates the guest
                    var result = await _userManager.CreateAsync(NewGuest, "Password123*");
                    await _db.SaveChangesAsync();
                    if (result.Succeeded)
                    {
                        //puts the new user in the guest role
                        await _userManager.AddToRoleAsync(NewGuest, "Customer");
                        if (result.Succeeded)
                        {
                            //activate the booking and give the user instruction on what to do next
                            booking.Isactivated = true;
                            await _db.SaveChangesAsync();
                            ViewBag.success = "Your account has been created with your email and your password is Password123*";
                            return View();
                        }
                    }
                }
                    
            }

            ViewBag.error = "Invalid code";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
