using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IAuthenticationService _auth;
    private readonly IDialogService _dialogs;
    [ObservableProperty] private string phone = "+237 ";
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private string confirmPassword = string.Empty;
    [ObservableProperty] private string selectedRole = "Ménage";

    public RegisterViewModel(INavigationService nav, IConnectivityService conn, IAuthenticationService auth, IDialogService dialogs) : base(nav,conn) { _auth=auth; _dialogs=dialogs; }

    [RelayCommand]
    private void SelectRole(string role) => SelectedRole = role;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        await RunAsync(async () =>
        {
            if (SelectedRole != "Ménage") { await _dialogs.AlertAsync("Compte géré", "Les comptes Ramasseur et Administrateur sont créés par l’administrateur du service."); return; }
            if (Password != ConfirmPassword) { ErrorMessage = "Les mots de passe ne correspondent pas."; return; }
            var result = await _auth.RegisterHouseholdAsync(Phone.Replace(" ", string.Empty), Password, "Fredi");
            if (!result.Success) { ErrorMessage = result.Message; return; }
            await Navigation.GoToAsync(Routes.Neighborhood);
        });
    }

    [RelayCommand]
    private async Task LoginAsync() => await Navigation.GoToAsync(Routes.Login);
}
