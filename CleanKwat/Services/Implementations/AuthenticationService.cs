using CleanKwat.Data;
using CleanKwat.Helpers;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppDatabase _db;
    private readonly IAppStateService _state;
    public User? CurrentUser => _state.CurrentUser;
    public AuthenticationService(AppDatabase db, IAppStateService state) { _db = db; _state = state; }

    public async Task<(bool Success, string Message, User? User)> LoginAsync(string identifier, string password)
    {
        var user = await _db.FindUserAsync(identifier.Trim());
        if (user is null || user.PasswordHash != PasswordHasher.Hash(password)) return (false, "Identifiant ou mot de passe incorrect.", null);
        if (!user.IsActive) return (false, "Ce compte est inactif.", null);
        _state.CurrentUser = user;
        await SecureStorage.Default.SetAsync("cleankwat_user_id", user.Id.ToString());
        return (true, "Connexion réussie.", user);
    }

    public async Task<(bool Success, string Message, User? User)> RegisterHouseholdAsync(string phone, string password, string displayName)
    {
        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password)) return (false, "Téléphone et mot de passe requis.", null);
        if (await _db.FindUserAsync(phone.Trim()) is not null) return (false, "Un compte existe déjà avec ce numéro.", null);
        var user = new User{Identifier=phone.Trim(),DisplayName=string.IsNullOrWhiteSpace(displayName)?"Ménage":displayName.Trim(),PasswordHash=PasswordHasher.Hash(password),Role=UserRole.Household.ToString()};
        await _db.InsertUserAsync(user);
        _state.CurrentUser = user;
        await SecureStorage.Default.SetAsync("cleankwat_user_id", user.Id.ToString());
        return (true, "Compte créé.", user);
    }

    public async Task LogoutAsync()
    {
        _state.CurrentUser = null; _state.CurrentHousehold = null; _state.SelectedNeighborhood = null;
        SecureStorage.Default.Remove("cleankwat_user_id");
        await Task.CompletedTask;
    }

    public async Task<User?> RestoreSessionAsync()
    {
        var raw = await SecureStorage.Default.GetAsync("cleankwat_user_id");
        if (!int.TryParse(raw, out var id)) return null;
        var user = await _db.GetUserAsync(id);
        _state.CurrentUser = user;
        return user;
    }
}
