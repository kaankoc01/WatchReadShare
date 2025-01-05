using Microsoft.EntityFrameworkCore;
using System;
using WatchReadShare.Domain.Entities;
using WatchReadShare.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Veritabaný baðlantýsýný yapýlandýr
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();


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
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
