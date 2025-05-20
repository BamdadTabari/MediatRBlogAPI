using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataLayer;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Apply Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<UserRole>().HasData(UserRoleSeed.All);
        modelBuilder.Entity<Role>().HasData(RoleSeed.All);
        modelBuilder.Entity<User>().HasData(UserSeed.All);
        modelBuilder.Entity<Option>().HasData(OptionSeed.All);

        // Creating Model
        base.OnModelCreating(modelBuilder);
    }
}