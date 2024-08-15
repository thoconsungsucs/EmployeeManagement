using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(c => c.CityId);
            modelBuilder.Entity<District>().HasKey(d => d.DistrictId);
            modelBuilder.Entity<Ward>().HasKey(w => w.WardId);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<District>()
                .HasMany(d => d.Wards)
                .WithOne(w => w.District)
                .HasForeignKey(w => w.DistrictId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Name = "Hanoi" },
                new City { CityId = 2, Name = "Ho Chi Minh" }
            );

            modelBuilder.Entity<District>().HasData(
                new District { DistrictId = 1, Name = "Dong Da", CityId = 1 },
                new District { DistrictId = 2, Name = "Cau Giay", CityId = 1 },
                new District { DistrictId = 3, Name = "Tan Binh", CityId = 2 },
                new District { DistrictId = 4, Name = "Binh Thanh", CityId = 2 }
            );

            modelBuilder.Entity<Ward>().HasData(
                new Ward { WardId = 1, Name = "Quan Thanh", DistrictId = 1 },
                new Ward { WardId = 2, Name = "Cat Linh", DistrictId = 1 },
                new Ward { WardId = 3, Name = "Nghia Do", DistrictId = 2 },
                new Ward { WardId = 4, Name = "Nghia Tan", DistrictId = 2 },
                new Ward { WardId = 5, Name = "Tan Dinh", DistrictId = 3 },
                new Ward { WardId = 6, Name = "Tan Thanh", DistrictId = 3 },
                new Ward { WardId = 7, Name = "Binh Thanh", DistrictId = 4 },
                new Ward { WardId = 8, Name = "Binh Loi", DistrictId = 4 }
            );
        }
    }
}
