using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelGuestSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CurrentRatesModel> GBPConversionRates { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
