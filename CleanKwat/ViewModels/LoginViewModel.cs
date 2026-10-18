using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthenticationService _auth;
    private readonly IAppStateService _state;
    private readonly IAppDataService _data;
    private readonly IDialogService _dialogs;
    [ObservableProperty] private string identifier = "+237677777777";
    [ObservableProperty] private string password = "Clean123!";

    public LoginViewModel(INavigationService nav, IConnectivityService conn, IAuthenticationService auth, IAppStateService state, IAppDataService data, IDialogService dialogs) : base(nav,conn) { _auth=auth; _state=state; _data=data; _dialogs=dialogs; }

    [RelayCommand]
    private async Task LoginAsync()
    {
        await RunAsync(async () =>
        {
            var result = await _auth.LoginAsync(Identifier.Trim(), Password);
            if (!result.Success || result.User is null) { ErrorMessage = result.Message; return; }
            _state.CurrentUser = result.User;
            if (result.User.Role == UserRole.Household.ToString())
            {
                var h = await _data.GetHouseholdByUserAsync(result.User.Id); _state.CurrentHousehold = h;
                await Navigation.GoToAsync(h is null ? Routes.Neighborhood : Routes.HouseholdHome);
            }
            else if (result.User.Role == UserRole.Collector.ToString()) await Navigation.GoToAsync(Routes.CollectorDashboard);
            else await Navigation.GoToAsync(Routes.AdminDashboard);
        });
    }

    [RelayCommand] private async Task RegisterAsync() => await Navigation.GoToAsync(Routes.Register);
    [RelayCommand] private async Task ForgotAsync() => await _dialogs.AlertAsync("Mot de passe oublié", "Pour cette démonstration académique, contactez l’administrateur du service.");
    [RelayCommand] private Task ExternalLoginAsync(string provider) => _dialogs.AlertAsync("Connexion externe", $"La connexion {provider} est présentée dans la maquette mais n’est pas requise pour cette démonstration académique.");
}
