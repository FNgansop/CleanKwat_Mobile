using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class SubscriptionViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data;
    [ObservableProperty] private Subscription? subscription;
    public SubscriptionViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data):base(nav,conn){_state=state;_data=data;}
    public async Task LoadAsync()=>await RunAsync(async()=>{var h=_state.CurrentHousehold;if(h is not null)Subscription=await _data.GetSubscriptionAsync(h.Id);});
    [RelayCommand] private Task Back()=>Navigation.GoBackAsync();
}
