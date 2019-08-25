using Microsoft.EntityFrameworkCore;
using Hometel.Domain.Models;

namespace Hometel.Persistence {
    public class AppDbContext : DbContext {
        public DbSet<User> Users {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<Amenity> Amenities {get; set;}
        public DbSet<Apartment> Apartments {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Guest> Guests {get; set;}
        public DbSet<Host> Hosts {get; set;}
        public DbSet<Location> Locations {get; set;}
        public DbSet<Reservation> Reservations {get; set;}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.Username);
            builder.Entity<User>().Property(u => u.Username).IsRequired();
            builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            builder.Entity<User>().Property(u => u.PasswordSalt).IsRequired();
            builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Surname).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Role).IsRequired();
            builder.Entity<User>().Property(u => u.Gender).IsRequired();

            //Host
            builder.Entity<Host>().HasMany(h => h.ListOfApartments).WithOne(appartment => appartment.Host).HasForeignKey(appartment => appartment.HostId);
        }
    }
}