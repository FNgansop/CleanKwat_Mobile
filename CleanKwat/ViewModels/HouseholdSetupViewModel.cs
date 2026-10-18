using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class HouseholdSetupViewModel : BaseViewModel
{
    private readonly IAppDataService _data; private readonly IAppStateService _state;
    [ObservableProperty] private int householdSize=4;
    [ObservableProperty] private string housingType="Maison individuelle";
    public Neighborhood? Neighborhood => _state.SelectedNeighborhood;
    public string NeighborhoodLabel => Neighborhood is null ? "Aucun quartier" : $"{Neighborhood.Name}
{Neighborhood.CityDistrict}";
    public HouseholdSetupViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data,IAppStateService state):base(nav,conn){_data=data;_state=state;}
    [RelayCommand] private void Increment()=>HouseholdSize=Math.Min(20,HouseholdSize+1);
    [RelayCommand] private void Decrement()=>HouseholdSize=Math.Max(1,HouseholdSize-1);
    [RelayCommand] private void SelectHousing(string type)=>HousingType=type;
    [RelayCommand] private async Task SaveAsync(){await RunAsync(async()=>{if(_state.CurrentUser is null||Neighborhood is null){ErrorMessage="Session ou quartier manquant.";return;}var h=new Household{UserId=_state.CurrentUser.Id,NeighborhoodId=Neighborhood.Id,HouseholdSize=HouseholdSize,HousingType=HousingType};await _data.SaveHouseholdAsync(h);_state.CurrentHousehold=h;await Navigation.GoToAsync(Routes.HouseholdHome);});}
}
