using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class ConfirmCollectionViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IAppDataService _data; private readonly IDialogService _dialogs;
    [ObservableProperty] private TourStop? stop;
    [ObservableProperty] private string notes="Déchets collectés. Ménage présent et coopératif.";
    private int _tourId;
    public ConfirmCollectionViewModel(INavigationService nav,IConnectivityService conn,IAppDataService data,IDialogService dialogs):base(nav,conn){_data=data;_dialogs=dialogs;}
    public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("tourId",out var tr)&&int.TryParse(tr?.ToString(),out var tid)){_tourId=tid;_ = LoadAsync(query);}}
    private async Task LoadAsync(IDictionary<string,object> query)=>await RunAsync(async()=>{if(!query.TryGetValue("stopId",out var raw)||!int.TryParse(raw?.ToString(),out var sid))return;Stop=(await _data.GetTourStopsAsync(_tourId)).FirstOrDefault(x=>x.Id==sid);});
    [RelayCommand] private async Task ConfirmAsync(){if(Stop is null)return;Stop.IsCollected=true;Stop.Notes=Notes;Stop.PendingSync=!Connectivity.IsOnline;await _data.UpdateTourStopAsync(Stop);await _dialogs.AlertAsync("Collecte enregistrée","La collecte a été ajoutée à votre tournée.");await Navigation.GoBackAsync();}
}
