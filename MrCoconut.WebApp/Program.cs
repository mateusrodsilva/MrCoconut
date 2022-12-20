using MrCoconut.WebApp.Infra;
using MrCoconut.WebApp.Infra.Repositories;
using MrCoconut.WebApp.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MrCoconut.WebApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
DatabaseConnection.ConnectionString = builder.Configuration.GetSection("MrCoconutDatabase:ConnectionString").Value;
DatabaseConnection.DatabaseName = builder.Configuration.GetSection("MrCoconutDatabase:DatabaseName").Value;
DatabaseConnection.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MrCoconutDatabase:IsSSL").Value);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserRepository, UserRepository>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "mrcoconut",
                        ValidAudience = "mrcoconut",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mrcoconut-authentication")),

                    };
                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
