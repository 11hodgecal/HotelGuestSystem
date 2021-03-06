using HotelGuestSystem.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<RoomServiceModel> RoomServiceItems { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<RequestModel> request { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
            SeedAdmin(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
            SeedCustomer(builder);
            SeedServices(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "78bf8cbe-1f70-4d6d-890b-247bc57e6150",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29"

                }
                );

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole()
               {
                   Id = "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02",
                   Name = "Customer",
                   NormalizedName = "Customer".ToUpper(),
                   ConcurrencyStamp = "d4e41d27-8605-4e69-8587-2636ed98e286"

               }
               );

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole()
               {
                   Id = "709a40af-4a4e-40b6-887b-d30dcdf07030",
                   Name = "Staff",
                   NormalizedName = "Staff".ToUpper(),
                   ConcurrencyStamp = "db72e6db-01bf-432b-8675-1d08242bb162"

               }
               );
        }
        private void SeedServices(ModelBuilder builder)
        {
            builder.Entity<RoomServiceModel>().HasData(
                new RoomServiceModel()
                {
                    Id = 1,
                    Name = "Toilet Roll",
                    Price = 0,
                    ServiceType = "RoomService"
                }
                ); 

            builder.Entity<RoomServiceModel>().HasData(
               new RoomServiceModel()
               {
                   Id = 2,
                   Name = "Fresh Towels",
                   Price = 0,
                   ServiceType = "RoomService"
               }
               );

            builder.Entity<RoomServiceModel>().HasData(
               new RoomServiceModel()
               {
                   Id = 3,
                   Name = "Change Sheets",
                   Price = 0,
                   ServiceType = "RoomService"
               }
               );
        }



        private void SeedAdmin(ModelBuilder builder)
        {
            PasswordHasher<UserModel> hasher = new PasswordHasher<UserModel>();
            UserModel user = new UserModel();
            user.Id = "27b9af34-a133-43e2-8dd2-aef04ddb2b8c";
            user.UserName = "admin@admin.com";
            user.NormalizedUserName = "admin@admin.com".ToUpper();
            user.NormalizedEmail = "admin@admin.com".ToUpper();
            user.Email = "admin@admin.com";
            user.Fname = "Bob";
            user.Sname = "Nobody";
            user.LockoutEnabled = false;
            user.ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29";
            user.PasswordHash = hasher.HashPassword(user, "Admin123!");

            builder.Entity<UserModel>().HasData(user);

        }
        private void SeedCustomer(ModelBuilder builder)
        {
            PasswordHasher<UserModel> hasher = new PasswordHasher<UserModel>();
            UserModel user = new UserModel();
            user.Id = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c";
            user.UserName = "Test@Test.com";
            user.NormalizedUserName = "Test@Test.com".ToUpper();
            user.NormalizedEmail = "Test@Test.com".ToUpper();
            user.Email = "Test@Test.com";
            user.Fname = "Bob";
            user.Sname = "Nobody";
            user.LockoutEnabled = false;
            user.ConcurrencyStamp = "7b483dfe-e56c-4d5b-97cd-b32652794d29";
            user.PasswordHash = hasher.HashPassword(user, "Admin123!");
            user.PreferedCurrency = "Yen";

            builder.Entity<UserModel>().HasData(user);

        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>()
                {
                    RoleId = "78bf8cbe-1f70-4d6d-890b-247bc57e6150",
                    UserId = "27b9af34-a133-43e2-8dd2-aef04ddb2b8c"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02",
                    UserId = "27b9df34-a133-43e2-8dd2-aef04ddb2b8c"
                });

        }
    }
}

