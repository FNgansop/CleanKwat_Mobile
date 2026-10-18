using CleanKwat.Models;
namespace CleanKwat.Services.Interfaces;
public interface IAuthenticationService
{
    User? CurrentUser { get; }
    Task<(bool Success, string Message, User? User)> LoginAsync(string identifier, string password);
    Task<(bool Success, string Message, User? User)> RegisterHouseholdAsync(string phone, string password, string displayName);
    Task LogoutAsync();
    Task<User?> RestoreSessionAsync();
}
