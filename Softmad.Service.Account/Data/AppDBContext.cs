using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Softmad.Service.Account.Models;

namespace Softmad.Service.Account.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Additional configurations for ApplicationUser entity if needed
        builder.Entity<AppUser>()
            .Property(u => u.FirstName)
            .HasMaxLength(50);

        builder.Entity<AppUser>()
            .Property(u => u.LastName)
            .HasMaxLength(50);

        builder.Entity<AppUser>()
            .Property(u => u.EmployeeID)
            .HasMaxLength(20);

        // Configure other properties as needed
    }
}

