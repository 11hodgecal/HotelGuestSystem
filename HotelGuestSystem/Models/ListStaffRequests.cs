using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public static class ListStaffRequests
    {
        public static List<StaffRequestViewModel> CreateView(List<RequestModel> requests, ApplicationDbContext db)
        {
            //creates a list of requests for the view
            List <StaffRequestViewModel> ConvertedRequest = new List <StaffRequestViewModel>();

            foreach (RequestModel request in requests)
            {
                //creates a view for a request
                StaffRequestViewModel viewModel = new StaffRequestViewModel();
                //Adds the request id whether its delivered and when the request was made
                viewModel.RequestId = request.Id;
                viewModel.IsDelivered = request.Delivered;
                viewModel.RequestMade = request.requestmade;
                viewModel.RequestMadestring = request.requestmade.ToString("hh:mm tt");
                var services = db.RoomServiceItems.ToListAsync().Result;
                var users = db.UserModel.ToListAsync().Result;
                //gets the name of the service being requested
                foreach (var service in services)
                {
                    if(service.Id == request.ItemID)
                    {
                        viewModel.ItemName = service.Name;
                    }
                }
                //gets the name of the customer that made the request
                foreach (var user in users)
                {
                    if (user.Id == request.CustomerID)
                    {
                        viewModel.CustomerName = user.Fullname();
                    }
                }
                //adds it to the view list
                ConvertedRequest.Add(viewModel);
            }
            //returns the to the view apart from any that are already delivered
            return ConvertedRequest.Where(s=> s.IsDelivered == false).OrderBy(s=> s.RequestMade).ToList();
        }
        public static async Task CompleteRequest(int requestid, ApplicationDbContext db)
        {
            //gets the request with the selected id
            var CompletedRequest = db.request.Where(s => s.Id == requestid).FirstOrDefault();

            await UpdateReceipt.Additem(CompletedRequest, db);

            //Marks the request as being completed
            CompletedRequest.Delivered = true;
            await db.SaveChangesAsync();  
            
        }
    }
}
