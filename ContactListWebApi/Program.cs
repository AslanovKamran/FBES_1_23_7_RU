using Microsoft.EntityFrameworkCore;
using ContactListWebApi.Data;
using System.Reflection;
using ContactListWebApi.Repository;
using ContactListWebApi.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


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

#region Jwt Options 

var jwtOptions = builder.Configuration.GetSection("Jwt");
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["Key"]!));

const double AccessTokenLifeTime = 3;

builder.Services.Configure<JwtOptions>(options =>
{
    options.Issuer = jwtOptions["Issuer"]!;
    options.Audience = jwtOptions["Audience"]!;
    options.AccessValidFor = TimeSpan.FromHours(AccessTokenLifeTime);
    options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
});


#endregion

#region EF Core Settings

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

#endregion

#region Builder Services

builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
#endregion

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
