
namespace DataLayer;

public static class RoleSeed
{
    public static List<Role> All =>
    [
        new()
        {
            Id = 1,
            Title = "Admin",
            CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            UpdatedAt = DateTime.Now,
            Slug = "Admin_Role"
        },
         new()
        {
            Id = 2,
            Title = "Customer",
            CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            Slug = "Customer_Role"
        }
    ];
}