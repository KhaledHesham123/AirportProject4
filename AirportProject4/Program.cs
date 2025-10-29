using AirportProject4.CQRS.CQRSContracts;
using AirportProject4.CQRS.Flights.FlightOrchestor;
using AirportProject4.Project.core;
using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.MappingProfiles;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.core.ServiceContrect;
using AirportProject4.Project.Repo;
using AirportProject4.Project.Repo.Data.Context;
using AirportProject4.Project.Repo.Repositories;
using AirportProject4.Project.Service;
using AirportProject4.Shared.attachmentService;
using AirportProject4.Shared.DbInitializerService;
using AirportProject4.Shared.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AirportProject4.Project.Service.FlightAvailabilityService;

namespace AirportProject4
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AirlineDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                /*.LogTo(log=>Debug.WriteLine(log),LogLevel.Information)*/;

            });

            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
               option.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                   ValidateAudience = true,
                   ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:AuthKey"] ?? string.Empty))

               }
               );

            builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AirlineDbContext>()
    .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            });
            builder.Services.AddAutoMapper(x => x.AddProfile(new FlightProfile()));

            builder.Services.AddScoped(typeof(IRepo<>), typeof(Repository<>));

            builder.Services.AddScoped<IunitofWork, UnitOfwork>();
            builder.Services.AddScoped<IFlightRepo, FlyightRepo>();
            builder.Services.AddScoped<IUpdateFlightsCommandOrchestrator, FlightUpdateOrchestor>();
            builder.Services.AddScoped<IFlightAvailabilityService, FlightAvailabilityServices>();
            builder.Services.AddScoped<IFlightBookOrchestor, FlightBookOrchestor>();

            builder.Services.AddScoped<IattachmentService, attachmentService>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();


            builder.Services.AddScoped<TransactionMiddlerWare>();

            builder.Services.AddScoped<IJwtService, JwtService>();


            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });


            var app = builder.Build();

            app.UseStaticFiles();
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbcontext = service.GetRequiredService<AirlineDbContext>();
            var dbInitializer = service.GetRequiredService<IDbInitializer>();

            try
            {
                _dbcontext.Database.Migrate();
                await dbInitializer.InitializeIdentityAsync();

            }
            catch (Exception ex)
            {

                var logger = service.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An Error Occurred During Apply the Migration");
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapGet("/{name}", async context =>
            {
                var name = context.GetRouteValue("name");
                await context.Response.WriteAsync($"Heloo{name}");
            });

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acount}/{action=Signin}/{id?}")
    .WithStaticAssets();

            app.Run();
        }
    }
}
