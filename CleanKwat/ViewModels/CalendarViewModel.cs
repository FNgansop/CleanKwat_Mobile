using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CalendarViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data;
    public ObservableCollection<CalendarDayItem> Days { get; }=new();
    public ObservableCollection<CollectionSchedule> Collections { get; }=new();
    public string MonthTitle => "Mai 2024";
    public CalendarViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data):base(nav,conn){_state=state;_data=data;BuildDays();}
    private void BuildDays(){Days.Clear();var first=new DateTime(2024,5,1);var offset=((int)first.DayOfWeek+6)%7;for(int i=0;i<offset;i++)Days.Add(new CalendarDayItem{IsEmpty=true});for(int d=1;d<=31;d++)Days.Add(new CalendarDayItem{Day=d,HasCollection=d is 15 or 22 or 29,IsSelected=d==15});}
    public async Task LoadAsync(){await RunAsync(async()=>{Collections.Clear();var h=_state.CurrentHousehold;if(h is null&&_state.CurrentUser is not null)h=await _data.GetHouseholdByUserAsync(_state.CurrentUser.Id);if(h is null)return;foreach(var c in (await _data.GetCollectionsAsync(h.Id)).Where(x=>x.Date.Year==2024&&x.Date.Month==5))Collections.Add(c);});}
    [RelayCommand] private Task Open(CollectionSchedule item)=>Navigation.GoToAsync($"{Routes.CollectionDetail}?id={item.Id}");
}
