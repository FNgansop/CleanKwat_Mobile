using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class ReportViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data; private readonly IDialogService _dialogs;
    public ObservableCollection<Report> Reports {get;}=new();
    [ObservableProperty] private string description="Le ramasseur n'est pas passé dans mon quartier.";
    [ObservableProperty] private string photoPath=string.Empty;
    public ReportViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data,IDialogService dialogs):base(nav,conn){_state=state;_data=data;_dialogs=dialogs;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Reports.Clear();var h=_state.CurrentHousehold;if(h is null)return;foreach(var r in await _data.GetReportsAsync(h.Id))Reports.Add(r);});
    [RelayCommand] private async Task PickPhotoAsync(){try{var file=await MediaPicker.Default.PickPhotoAsync();if(file is not null)PhotoPath=file.FullPath;}catch(Exception ex){await _dialogs.AlertAsync("Photo",ex.Message);}}
    [RelayCommand] private async Task SubmitAsync(){await RunAsync(async()=>{var h=_state.CurrentHousehold;if(h is null)return;if(Description.Trim().Length<20){ErrorMessage="Veuillez compléter la description (20 caractères minimum).";return;}var collection=(await _data.GetCollectionsAsync(h.Id)).OrderBy(x=>x.Date).FirstOrDefault();var report=new Report{HouseholdId=h.Id,CollectionScheduleId=collection?.Id??0,Description=Description.Trim(),PhotoPath=PhotoPath,CreatedAt=DateTime.Now,Status=ReportStatus.Open.ToString(),PendingSync=!Connectivity.IsOnline};await _data.AddReportAsync(report);await _dialogs.AlertAsync("Signalement envoyé","Merci ! Votre signalement a été transmis.");await LoadAsync();});}
}
