using System;

namespace HotelGuestSystem.Models
{
    public class StaffRequestViewModel
    {
        public int RequestId { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime RequestMade { get; set; }
        public string RequestMadestring { get; set; }

    }
}
