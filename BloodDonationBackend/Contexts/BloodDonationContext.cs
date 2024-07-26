using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BloodDonationBackend.Contexts
{
    public class BloodDonationContext : DbContext
    {
        public BloodDonationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<BloodInventory> BloodInventorys { get; set; }
        public DbSet<DonationCenter> DonationCenters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
