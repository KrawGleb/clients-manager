using ClientsManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<OrderInfo> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ToDo: Add indexes

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.Id)
            .UseMySqlIdentityColumn()
            .IsRequired();

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.FirstName)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.LastName)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.AdditionalName)
            .HasColumnType("nvarchar(100)")
            .IsRequired(false);

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.PhoneNumber)
            .HasColumnType("nvarchar(30)")
            .IsRequired();

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.CarModel)
            .HasColumnType("nvarchar(50)")
            .IsRequired(false);

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.CarNumber)
            .HasColumnType("nvarchar(20)")
            .IsRequired(false);

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.Description)
            .HasColumnType("nvarchar(500)")
            .IsRequired(false);

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.Price)
            .HasColumnType("decimal(15, 2)");

        modelBuilder
            .Entity<OrderInfo>()
            .Property(o => o.OrderType)
            .HasColumnType("tinyint")
            .IsRequired(true);
    }
}
