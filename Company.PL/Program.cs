using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Company.PL.Mapping;
using Company.PL.Services;
//using Company.PL.Settings;
using Compnay.BLL;
using Compnay.BLL.Interfaces;
using Compnay.BLL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Bulit-in MVC Services

            //builder.Services.AddScoped<IDepartmentReopsitory,DepartmentReopsitory>(); // Allow DI For DepartmentReopsitory
            //builder.Services.AddScoped<IEmployeeReopsitory, EmployeeReopsitory>(); // Allow DI For DepartmentReopsitory

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            } );  // Allow DI For CompanyDbContext

            // Life Time
            //builder.Services.AddScoped();    // Create Object Life Time Per Request - UnReachable Object
            //builder.Services.AddTransient(); // Create Object Life Time Per Operation
            //builder.Services.AddSingleton(); // Create Object Life Time Per App
            builder.Services.AddScoped<IScopedServices, ScopedServices>(); // per request
            builder.Services.AddTransient<ITransentServices,TransentServices>(); // per operation
            builder.Services.AddSingleton<ISingletonServices, SingletonServices>(); // per app
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();

            //builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });




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

            app.UseAuthentication();

            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}


#region Asy vs syn
class Test
{
    public void Fun01()
    {
        // Statment01
        // Statment02
        // Statment03 --> Take Time
        // Statment04
        // Statment05
    }

    public void Fun02()
    {
        // Statment01
        // Statment02
        // Statment03 --> Take Time
        // Statment04
        // Statment05
    }
}
#endregion