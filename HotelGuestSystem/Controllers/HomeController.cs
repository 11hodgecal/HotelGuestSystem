using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<UserModel> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //allows a Guest to check into the system with a booking code
        [HttpPost]
        public async Task<IActionResult> Index(string code)
        {
            //checks whether the booking code is present and gives an error
            if (code == null)
            {
                ViewBag.error = "Please enter a booking code";
                return View();
            }
            //checks whether the booking code has a correct ammount of characters and gives an error
            if (code.Length != 10)
            {
                ViewBag.error = "Please enter a 10 character long booking code";
                return View();
            }
            //gets all the current bookings
            var bookings = await _db.Bookings.ToListAsync();
            foreach (var booking in bookings)
            {
                //finds the specific booking matching the user input
                if(booking.BookingCode == code)
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
                    //attach the booking to the user
                    NewGuest.Bookingid = booking.Id;
                    
                    //creates the guest
                    var result = await _userManager.CreateAsync(NewGuest, booking.BookingCode);
                    await _db.SaveChangesAsync();
                    if (result.Succeeded)
                    {
                        //puts the new user in the guest role
                        await _userManager.AddToRoleAsync(NewGuest, "Customer");
                        if (result.Succeeded)
                        {
                            //activate the booking and give the user instruction on what to do next
                            booking.Isactivated = true;
                            ViewBag.success = "Your account has been created with your email and your booking code as the password";
                            return View();
                        }
                    }
                }
            }
            
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
