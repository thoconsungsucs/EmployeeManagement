using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Interfaces.IRepositories;
using EmployeeManagement.Interfaces.IServices;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;
using EmployeeManagement.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddValidatorsFromAssemblyContaining<CityValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DistrictValidator>();

            builder.Services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            builder.Services.AddScoped(typeof(IDistrictRepository), typeof(DistrictRepository));
            builder.Services.AddScoped(typeof(IWardRepository), typeof(WardRepository));
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IDistrictService, DistrictService>();
            builder.Services.AddScoped<IWardService, WardService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
