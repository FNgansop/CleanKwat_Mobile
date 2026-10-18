using SQLite;

namespace CleanKwat.Models;

public class User
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Indexed] public string Identifier { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = UserRole.Household.ToString();
    public bool IsActive { get; set; } = true;
}
