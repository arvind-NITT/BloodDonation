using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Repositories;
using BloodDonationBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BloodDonationBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                  };

              });

            #region contexts
            builder.Services.AddDbContext<BloodDonationContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );
            #endregion

            #region repositories

            builder.Services.AddScoped<IRepository<int, User>, UserRepo>();
            builder.Services.AddScoped<IRepository<int, UserDetail>, UserDetailRepository>();
            builder.Services.AddScoped<IRepository<int, Donor>, DonorRepo>();
            builder.Services.AddScoped<IRepository<int, Recipient>, RecipientRepo>();
            builder.Services.AddScoped<IRepository<int, Appointment>, AppointmentRepo>();
            builder.Services.AddScoped<IRepository<int, BloodInventory>, BloodInventoryRepo>();
            builder.Services.AddScoped<IRepository<int, BloodRequest>, BloodRequestRepo>();
            builder.Services.AddScoped<IRepository<int, DonationCenter>, DonationCenterRepo>();

            #endregion

            #region services
            //builder.Services.AddScoped<IEmployeeService, EmployeeBasicService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDonorService, DonorService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IRecipientService, RecipientService>();
            //builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            //builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IAdminInterface, AdminService>();

            #endregion
            #region CORS
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
