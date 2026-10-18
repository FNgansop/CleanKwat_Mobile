using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class HouseholdHomeViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data; private readonly ISyncService _sync;
    [ObservableProperty] private string userName="Fredi";
    [ObservableProperty] private string location="Bonanjo, Douala 1er";
    [ObservableProperty] private string nextCollectionDate="Ven. 09 Octobre 2026";
    [ObservableProperty] private string nextCollectionTime="Entre 06H et 10H";
    [ObservableProperty] private string planName="Standard";
    [ObservableProperty] private string planPrice="2 000 F / mois";
    [ObservableProperty] private string syncLabel="Dernière mise à jour : --";
    public HouseholdHomeViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data,ISyncService sync):base(nav,conn){_state=state;_data=data;_sync=sync;}
    public async Task LoadAsync(){await RunAsync(async()=>{var u=_state.CurrentUser; if(u is not null) UserName=u.DisplayName; var h=_state.CurrentHousehold ?? (u is null?null:await _data.GetHouseholdByUserAsync(u.Id)); if(h is not null){_state.CurrentHousehold=h;var n=await _data.GetNeighborhoodAsync(h.NeighborhoodId);if(n is not null)Location=$"{n.Name}, {n.CityDistrict}";var s=await _data.GetSubscriptionAsync(h.Id);if(s is not null){PlanName=s.PlanName;PlanPrice=$"{s.MonthlyAmount:N0} F / mois".Replace(","," ");}var col=(await _data.GetCollectionsAsync(h.Id)).OrderByDescending(x=>x.Date).FirstOrDefault();if(col is not null){NextCollectionDate=col.Date.ToString("ddd. dd MMMM yyyy",new System.Globalization.CultureInfo("fr-FR"));NextCollectionTime=$"Entre {col.TimeWindow}";}}if(await _sync.SyncAsync())SyncLabel=$"Dernière mise à jour : {DateTime.Now:HH'h'mm}";else SyncLabel="Mode hors ligne";});}
    [RelayCommand] private Task OpenSubscription()=>Navigation.GoToAsync(Routes.Subscription);
    [RelayCommand] private Task OpenCalendar()=>Navigation.GoToAsync(Routes.Calendar);
    [RelayCommand] private Task OpenPayments()=>Navigation.GoToAsync(Routes.Payments);
    [RelayCommand] private Task OpenReport()=>Navigation.GoToAsync(Routes.Report);
    [RelayCommand] private Task OpenNotifications()=>Navigation.GoToAsync(Routes.Notifications);
}
