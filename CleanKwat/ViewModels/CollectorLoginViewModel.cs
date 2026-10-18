using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CollectorLoginViewModel : BaseViewModel
{
    private readonly IAuthenticationService _auth;
    [ObservableProperty] private string identifier="collector@cleankwat.local";
    [ObservableProperty] private string password="Collector123!";
    public CollectorLoginViewModel(INavigationService nav,IConnectivityService conn,IAuthenticationService auth):base(nav,conn)=>_auth=auth;
    [RelayCommand] private async Task LoginAsync(){await RunAsync(async()=>{var r=await _auth.LoginAsync(Identifier.Trim(),Password);if(!r.Success||r.User is null||r.User.Role!=UserRole.Collector.ToString()){ErrorMessage="Accès Ramasseur refusé.";return;}await Navigation.GoToAsync(Routes.CollectorDashboard);});}
}
