using Microsoft.EntityFrameworkCore;
using ContactListWebApi.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    #region Documentation Section

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    #endregion

});

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.DisplayRequestDuration());
}

app.UseCors(options =>
{
    options.AllowAnyMethod();   // Allows GET, POST, PUT, DELETE, etc.
    options.AllowAnyHeader();   // Allows any HTTP headers in requests
    options.AllowAnyOrigin();   // Allows requests from any domain
});

app.UseAuthorization();

app.MapControllers();

app.Run();
