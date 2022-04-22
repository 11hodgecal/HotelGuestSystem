using HotelGuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelGuestSystem.Models
{
    public static class AccountExpiredChecker
    {
        
        public static async Task DeleteExpired(ApplicationDbContext db)
        {
            //Gets the current bookings
            var Users = await db.UserModel.ToListAsync();
            var Bookings = await db.Bookings.ToListAsync();
            //finds the expired Guest and deletes them along with there bookings
            foreach (UserModel user in Users)
            {
                foreach (BookingModel booking in Bookings)
                {
                    if (user.Bookingid == booking.Id)
                    {
                        var isexpired = DateTime.Compare(DateTime.Now, booking.BookingEnd);
                        if (isexpired == 1)
                        {
                           
                            db.Users.Remove(user);
                            db.Bookings.Remove(booking);
                        }
                    }
                }
            }
            //saves chanages
            await db.SaveChangesAsync();
        }
    }
}
