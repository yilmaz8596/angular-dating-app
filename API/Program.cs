
using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("TokenKey not found");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAngularDev");

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
