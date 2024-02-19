using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;


builder.Services.AddDbContext<EmployeeDBContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("EmpDb")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EmployeeDBContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("7eqrDqKZ6vnIxFD0cTzFfJ0hl+Vm00BC42M9BHvr4yc=")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IQualificationRepository, QualificationRepository>();
builder.Services.AddScoped<IEmployeeDocumentRepository, EmployeeDocumentRepository>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DataSeeder.SeedAdminUser(userManager, roleManager);
}

app.Run();
