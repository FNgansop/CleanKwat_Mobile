using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class TourDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IAppDataService _data;
    [ObservableProperty] private Tour? tour;
    public ObservableCollection<TourStop> Stops{get;}=new();
    public TourDetailViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data):base(nav,conn)=>_data=data;
    public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("id",out var raw)&&int.TryParse(raw?.ToString(),out var id))_ = LoadAsync(id);}
    private async Task LoadAsync(int id)=>await RunAsync(async()=>{Tour=await _data.GetTourAsync(id);Stops.Clear();foreach(var s in await _data.GetTourStopsAsync(id))Stops.Add(s);});
    [RelayCommand] private Task Confirm(TourStop stop)=>Navigation.GoToAsync(Routes.ConfirmCollection,new Dictionary<string,object>{{"stopId",stop.Id},{"tourId",stop.TourId}});
}
