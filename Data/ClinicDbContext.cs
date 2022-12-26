using Microsoft.EntityFrameworkCore;
using Clinic.Models;

namespace Clinic.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        { }

        public DbSet<Admin> admins { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Specialization> specializations { get; set; }
        public DbSet<Сonclusion> сonclusions { get; set; }

        public DbSet<Time> times { get; set; }
    }
}
