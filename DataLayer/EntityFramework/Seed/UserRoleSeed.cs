namespace DataLayer;

public static class UserRoleSeed
{
    public static List<UserRole> All =>
    [
        new()
        {
            RoleId = 1,
            UserId = 1,
            CreatedAt =  new DateTime(2025, 1, 1, 12, 0, 0),
            UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            Slug = "Admin-User",
        }
    ];
}