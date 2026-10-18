using System.Collections.ObjectModel;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class AdminCollectorsViewModel : BaseViewModel
{
    private readonly IAppDataService _data;
    public ObservableCollection<User> Collectors{get;}=new();
    public AdminCollectorsViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data):base(nav,conn)=>_data=data;
    public async Task LoadAsync()=>await RunAsync(async()=>{Collectors.Clear();foreach(var u in await _data.GetCollectorsAsync())Collectors.Add(u);});
}
