using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class AdminDashboardViewModel : BaseViewModel
{
    [ObservableProperty] private int households=125;
    [ObservableProperty] private int collectors=8;
    [ObservableProperty] private int collections=320;
    [ObservableProperty] private int reports=18;
    public AdminDashboardViewModel(INavigationService nav,IConnectivityService conn):base(nav,conn){}
    [RelayCommand] private Task OpenNeighborhoods()=>Navigation.GoToAsync(Routes.AdminNeighborhoods);
    [RelayCommand] private Task OpenCollections()=>Navigation.GoToAsync(Routes.AdminCollections);
    [RelayCommand] private Task OpenCollectors()=>Navigation.GoToAsync(Routes.AdminCollectors);
}
