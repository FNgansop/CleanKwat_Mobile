using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CollectorDashboardViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data; private readonly ISyncService _sync;
    [ObservableProperty] private string collectorName="Joseph";
    [ObservableProperty] private int planned=12;
    [ObservableProperty] private int completed=8;
    [ObservableProperty] private int openReports=2;
    [ObservableProperty] private int todayTours=3;
    [ObservableProperty] private Tour? activeTour;
    [ObservableProperty] private string syncLabel="Dernière mise à jour : 09 h 30";
    public CollectorDashboardViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data,ISyncService sync):base(nav,conn){_state=state;_data=data;_sync=sync;}
    public async Task LoadAsync()=>await RunAsync(async()=>{if(_state.CurrentUser is not null)CollectorName=_state.CurrentUser.DisplayName;var uid=_state.CurrentUser?.Id??2;var tours=await _data.GetToursForCollectorAsync(uid);var todays=tours.Where(x=>x.Date.Date==new DateTime(2024,5,15)).ToList();TodayTours=todays.Count;ActiveTour=todays.FirstOrDefault(x=>x.Status==TourStatus.InProgress.ToString())??todays.FirstOrDefault();if(ActiveTour is not null){Planned=ActiveTour.PlannedCount;Completed=ActiveTour.CompletedCount;}if(await _sync.SyncAsync())SyncLabel=$"Dernière mise à jour : {DateTime.Now:HH 'h' mm}";else SyncLabel="Mode hors ligne";});
    [RelayCommand] private Task OpenTour()=>ActiveTour is null?Task.CompletedTask:Navigation.GoToAsync($"{Routes.TourDetail}?id={ActiveTour.Id}");
    [RelayCommand] private Task OpenTours()=>Navigation.GoToAsync(Routes.CollectorTours);
}
