using System.Collections.ObjectModel;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class NotificationsViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data;
    public ObservableCollection<AppNotification> Notifications{get;}=new();
    public NotificationsViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data):base(nav,conn){_state=state;_data=data;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Notifications.Clear();if(_state.CurrentUser is null)return;foreach(var n in await _data.GetNotificationsAsync(_state.CurrentUser.Id))Notifications.Add(n);});
}
