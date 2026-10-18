using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class OnboardingViewModel : BaseViewModel
{
    private readonly IAuthenticationService _auth;
    public OnboardingViewModel(INavigationService nav, IConnectivityService conn, IAuthenticationService auth) : base(nav, conn) => _auth = auth;

    [RelayCommand]
    private async Task ContinueAsync() => await Navigation.GoToAsync(Routes.Login);

    [RelayCommand]
    private async Task SkipAsync() => await Navigation.GoToAsync(Routes.Login);
}
