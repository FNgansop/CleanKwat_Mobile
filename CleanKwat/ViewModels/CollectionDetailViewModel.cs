using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CollectionDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IAppDataService _data;
    [ObservableProperty] private CollectionSchedule? collection;
    public CollectionDetailViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data):base(nav,conn)=>_data=data;
    public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("id",out var raw)&&int.TryParse(raw?.ToString(),out var id))_ = LoadAsync(id);}
    private async Task LoadAsync(int id)=>await RunAsync(async()=>Collection=await _data.GetCollectionAsync(id));
    [RelayCommand] private Task Back()=>Navigation.GoBackAsync();
}
