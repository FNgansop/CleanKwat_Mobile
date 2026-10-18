using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class NeighborhoodSelectionViewModel : BaseViewModel
{
    private readonly IAppDataService _data; private readonly IAppStateService _state;
    public ObservableCollection<Neighborhood> Neighborhoods { get; } = new();
    [ObservableProperty] private string searchText = string.Empty;
    [ObservableProperty] private Neighborhood? selectedNeighborhood;
    public NeighborhoodSelectionViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data,IAppStateService state):base(nav,conn){_data=data;_state=state;}
    public async Task LoadAsync() => await RunAsync(async()=>{Neighborhoods.Clear();foreach(var n in await _data.GetNeighborhoodsAsync(SearchText))Neighborhoods.Add(n);SelectedNeighborhood ??= Neighborhoods.FirstOrDefault(x=>x.Name=="Bonanjo");});
    partial void OnSearchTextChanged(string value) => _ = LoadAsync();
    [RelayCommand] private void Select(Neighborhood item)=>SelectedNeighborhood=item;
    [RelayCommand] private async Task ContinueAsync(){if(SelectedNeighborhood is null){ErrorMessage="Sélectionnez un quartier.";return;}_state.SelectedNeighborhood=SelectedNeighborhood;await Navigation.GoToAsync(Routes.HouseholdSetup);}
}
