using Microsoft.EntityFrameworkCore;
using ContactListWebApi.Data;
using System.Reflection;
using ContactListWebApi.Repository;
using ContactListWebApi.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

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

    #region Jwt Bearer Section

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Jwt Authentication",
        Description = "Type in a valid JWT Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme,Array.Empty<string>() }
    });

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

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ClockSkew = TimeSpan.Zero // Removes default additional time to the tokens
    };
});
#endregion

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
