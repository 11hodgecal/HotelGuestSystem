using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace HotelGuestSystem.Controllers
{
    public class RoomServiceManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;


        public RoomServiceManagementController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [Authorize(Roles = "Admin")]
        // GET: RoomServiceManagement
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomServiceItems.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        //allows the admin to load the create form
        public IActionResult Create()
        {
            return View();
        }

        //allows the admin to create a service or product
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,ServiceType,Price,img")] RoomServiceModel roomServiceModel)
        {
            try
            {
                //Adds the roomservice with the image
                if (ModelState.IsValid)
                {
                    var file = Request.Form.Files[0];

                    if (file != null)
                    {
                        //gets the uploaded image
                        var upload = Request.Form.Files[0];

                        //gets the images extension
                        string extenstion = Path.GetExtension(upload.FileName);

                        //gets the root path
                        string root = _webHostEnvironment.WebRootPath;

                        //sets the webpath
                        var webpath = $"/images/";
                        //sets the full path to the image
                        var path = $"{root}{webpath}";

                        //sets the new file name
                        var filename = $"{DateTime.Now.ToString("yymmssfff")}{extenstion}".ToLower();

                        //sets the new display image path 
                        roomServiceModel.Imagepath = $"{path}{filename}";

                        //creates the directory set by the path
                        Directory.CreateDirectory(path);

                        //uploads the new file to the directory
                        using (var filestream = new FileStream(roomServiceModel.Imagepath, FileMode.Create))
                        {
                            await upload.CopyToAsync(filestream);
                        }

                        //sets the new image path to the webpath followed by the filename
                        roomServiceModel.Imagepath = $"{webpath}{filename}";

                        //adds the new display image 
                        _context.Add(roomServiceModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception)
            {

                if (ModelState.IsValid)
                {
                        //adds the imageless room service
                        _context.Add(roomServiceModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    
                }
            }
            
            return View(roomServiceModel);
        }

        //gets the existing information on a item and fills the existing value into the view
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomServiceModel = await _context.RoomServiceItems.FindAsync(id);
            if (roomServiceModel == null)
            {
                return NotFound();
            }
            return View(roomServiceModel);
        }

        //allows the admin to change the price of a product or service
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ServiceType,Price,Imagepath")] RoomServiceModel roomServiceModel)
        {
            if (id != roomServiceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomServiceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomServiceModelExists(roomServiceModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomServiceModel);
        }
        
        //allows the admin to confirm deletion of a product
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomServiceModel = await _context.RoomServiceItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomServiceModel == null)
            {
                return NotFound();
            }

            return View(roomServiceModel);
        }
        //allows the Admin to delete a product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomServiceModel = await _context.RoomServiceItems.FindAsync(id);
            _context.RoomServiceItems.Remove(roomServiceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomServiceModelExists(int id)
        {
            return _context.RoomServiceItems.Any(e => e.Id == id);
        }
    }
}
