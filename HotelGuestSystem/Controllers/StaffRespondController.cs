using HotelGuestSystem.Data;
using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelGuestSystem.Controllers
{
    public class StaffRespondController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StaffRespondController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            //gets all requests
            var requests = await _db.request.ToListAsync();
            //create the view models for the active requests
            var view = ListStaffRequests.CreateView(requests, _db);
            return View(view);
        }
        public async Task<IActionResult> Complete(int id)
        {
            //complete the request
            await ListStaffRequests.CompleteRequest(id, _db);
            return RedirectToAction("Index");
        }
    }
}
