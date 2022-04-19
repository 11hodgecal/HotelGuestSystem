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

namespace ItManagementSystem.Controllers
{
    public class UserController : Controller
    {

        //imports db and the user manager 
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationDbContext context, UserManager<UserModel> Users, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = Users;
            _roleManager = roleManager;
        }

        //allows the Admin to list the users in the database
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }



        //allows the Admin to get the id via routing from the index and delete a user that is in the database
        [Authorize(Roles = "Admin")]
        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetModel == null)
            {
                return NotFound();
            }

            return View(assetModel);
        }
        //deletes the user with the routed id
        [Authorize(Roles = "Admin")]
        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var UserModel = await _context.UserModel.FindAsync(id);

            _context.UserModel.Remove(UserModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        //allows the manager to create new users
        [Authorize(Roles = "Admin")]
        // GET: Asset/Create
        public IActionResult Create()
        {

            return View();
        }

        //the manager is able to create a new user from user input that corrilates to the user instance then that instance becomes a new user with
        //a default password that can be changed and the adds them to the Support role.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Fname,Sname")] UserModel userModel)
        {
            userModel.UserName = userModel.Email;
            if (ModelState.IsValid)
            {
                    var result = await _userManager.CreateAsync(userModel, "P4assword123*");
                    await _context.SaveChangesAsync();
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userModel, "Staff");
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
               
            }
            return View(userModel);
        }
    }
}
