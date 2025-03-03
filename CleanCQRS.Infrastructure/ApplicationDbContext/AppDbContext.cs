using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Enums;
using CleanCQRS.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.ApplicationDbContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<User> TblUser { get; set; }
        public DbSet<Role> TblRole { get; set; }
        public DbSet<Doctor> TblDoctor { get; set; }
        public DbSet<ProvidedServices> TblProvidedServices { get; set; }
        public DbSet<DoctorService> TblDoctorService { get; set; }
        public DbSet<DoctorAvailability> TblDoctorAvailability { get; set; }
        public DbSet<Patient> TblPatient { get; set; }
        public DbSet<Appointment> TblAppointment { get; set; }
        public DbSet<AppointmentMedia> TblAppointmentMedia { get; set; }
        public DbSet<ExceptionLog> TblExceptionLogs { get; set; } // Add this


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // User Doctor relationship.
            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId);

            // Many-to-Many relationship configuration
            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorServices) // Assuming Doctor has a collection of DoctorServices
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if needed

            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.ProvidedService)
                .WithMany(s => s.DoctorServices) // Assuming ProvidedServices has a collection of DoctorServices
                .HasForeignKey(ds => ds.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if needed

            // User Patient relationship.
            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(d => d.User)
                .HasForeignKey<Patient>(d => d.UserId);

            //Appointment Configuration
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

            base.OnModelCreating(modelBuilder);

         

        }

    }
}
