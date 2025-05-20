using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer;

public class User : BaseEntity
{
    #region Identity

    public string Username { get; set; }

    public string Mobile { get; set; }
    public bool IsMobileComfirmed { get; set; }

    public string Email { get; set; }

    #endregion

    #region Login

    public string PasswordHash { get; set; }

    public int FailedLoginCount { get; set; }
    public DateTime? LockoutEndTime { get; set; }

    public DateTime? LastLoginDate { get; set; }

    #endregion

    #region Management

    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public bool IsLockedOut { get; set; }
    public bool IsActive { get; set; }
    public bool CanBuyBulk { get; set; }
    #endregion

    #region Navigations
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Basket> Baskets { get; set; }
    #endregion
}

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(b => b.Username).IsUnique();

        #region Mappings

        builder.Property(b => b.Mobile)
            .IsRequired();

        #endregion

        #region Navigations

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion
    }
}