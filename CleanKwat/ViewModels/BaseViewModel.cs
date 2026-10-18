using CommunityToolkit.Mvvm.ComponentModel;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected readonly INavigationService Navigation;
    protected readonly IConnectivityService Connectivity;

    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string errorMessage = string.Empty;
    [ObservableProperty] private bool isOffline;
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    protected BaseViewModel(INavigationService navigation, IConnectivityService connectivity)
    {
        Navigation = navigation;
        Connectivity = connectivity;
        IsOffline = !connectivity.IsOnline;
    }

    partial void OnErrorMessageChanged(string value) => OnPropertyChanged(nameof(HasError));

    protected async Task RunAsync(Func<Task> action)
    {
        if (IsBusy) return;
        try { IsBusy = true; ErrorMessage = string.Empty; IsOffline = !Connectivity.IsOnline; await action(); }
        catch (Exception ex) { ErrorMessage = ex.Message; }
        finally { IsBusy = false; }
    }
}
