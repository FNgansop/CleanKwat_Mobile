using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CollectorToursViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data;
    public ObservableCollection<Tour> Tours{get;}=new();
    public CollectorToursViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data):base(nav,conn){_state=state;_data=data;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Tours.Clear();var uid=_state.CurrentUser?.Id??2;foreach(var t in (await _data.GetToursForCollectorAsync(uid)).Where(x=>x.Date.Date==new DateTime(2024,5,15)).Take(3))Tours.Add(t);});
    [RelayCommand] private Task Open(Tour t)=>Navigation.GoToAsync($"{Routes.TourDetail}?id={t.Id}");
}
