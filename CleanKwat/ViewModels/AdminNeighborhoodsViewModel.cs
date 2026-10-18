using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class AdminNeighborhoodsViewModel : BaseViewModel
{
    private readonly IAppDataService _data; private readonly IDialogService _dialogs;
    public ObservableCollection<Neighborhood> Neighborhoods{get;}=new();
    [ObservableProperty] private string searchText=string.Empty;
    public AdminNeighborhoodsViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data,IDialogService dialogs):base(nav,conn){_data=data;_dialogs=dialogs;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Neighborhoods.Clear();var all=await _data.GetAdminNeighborhoodsAsync();foreach(var n in all.Where(x=>string.IsNullOrWhiteSpace(SearchText)||x.Name.Contains(SearchText,StringComparison.OrdinalIgnoreCase)).Take(20))Neighborhoods.Add(n);});
    partial void OnSearchTextChanged(string value)=>_ = LoadAsync();
    [RelayCommand] private async Task AddAsync(){var name=await _dialogs.PromptAsync("Ajouter un quartier","Nom du quartier",placeholder:"Ex. Deido");if(string.IsNullOrWhiteSpace(name))return;await _data.AddNeighborhoodAsync(new Neighborhood{Name=name.Trim(),CityDistrict="Douala",IsActive=true});await LoadAsync();}
    [RelayCommand] private async Task EditAsync(Neighborhood n){var name=await _dialogs.PromptAsync("Modifier le quartier","Nouveau nom",placeholder:n.Name);if(string.IsNullOrWhiteSpace(name))return;n.Name=name.Trim();await _data.UpdateNeighborhoodAsync(n);await LoadAsync();}
}
