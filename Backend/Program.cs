using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    using (var scope = app.Services.CreateScope())
    {
        var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();

        await roleService.CreateRoleAsync("Administrator");
        await roleService.CreateRoleAsync("Librarian");
        await roleService.CreateRoleAsync("Client");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating roles: {ex.Message}");
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();