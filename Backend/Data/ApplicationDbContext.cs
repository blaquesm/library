using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      public DbSet<Book> Books { get; set; }
      public DbSet<Reservation> Reservations { get; set; }
  }
}