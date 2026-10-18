using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class AdminCollectionsViewModel : BaseViewModel
{
    private readonly IAppDataService _data; private readonly IDialogService _dialogs;
    public ObservableCollection<Tour> Tours{get;}=new();
    [ObservableProperty] private string searchText=string.Empty;
    [ObservableProperty] private string selectedStatus="Toutes";
    public AdminCollectionsViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data,IDialogService dialogs):base(nav,conn){_data=data;_dialogs=dialogs;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Tours.Clear();var all=await _data.GetAllToursAsync();var q=all.AsEnumerable();if(!string.IsNullOrWhiteSpace(SearchText))q=q.Where(x=>x.Name.Contains(SearchText,StringComparison.OrdinalIgnoreCase));if(SelectedStatus=="En cours")q=q.Where(x=>x.Status==TourStatus.InProgress.ToString());else if(SelectedStatus=="Terminées")q=q.Where(x=>x.Status==TourStatus.Completed.ToString());else if(SelectedStatus=="Non réalisées")q=q.Where(x=>x.Status==TourStatus.Missed.ToString());foreach(var t in q.Take(20))Tours.Add(t);});
    partial void OnSearchTextChanged(string value)=>_ = LoadAsync();
    [RelayCommand] private async Task FilterAsync(string s){SelectedStatus=s;await LoadAsync();}
    [RelayCommand] private async Task AddAsync(){var name=await _dialogs.PromptAsync("Nouvelle tournée","Nom de la zone",placeholder:"Ex. Deido – Zone 1");if(string.IsNullOrWhiteSpace(name))return;await _data.AddTourAsync(new Tour{Name=name.Trim(),Date=DateTime.Today,TimeWindow="08 h – 12 h",PlannedCount=10,CompletedCount=0,Status=TourStatus.Planned.ToString(),CollectorUserId=2,NeighborhoodId=1});await LoadAsync();}
}
