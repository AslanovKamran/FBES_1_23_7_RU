using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Areas.Identity.Data;
using RazorPagesApp.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RazorPagesAppContextConnection") ?? throw new InvalidOperationException("Connection string 'RazorPagesAppContextConnection' not found.");

builder.Services.AddDbContext<RazorPagesAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RazorPagesAppContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
