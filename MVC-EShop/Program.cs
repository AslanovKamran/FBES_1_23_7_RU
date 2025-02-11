using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Add db context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#region EF Core DbContext

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();


app.MapControllerRoute(
     name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
