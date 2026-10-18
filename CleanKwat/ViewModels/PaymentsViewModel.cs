using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class PaymentsViewModel : BaseViewModel
{
    private readonly IAppStateService _state; private readonly IAppDataService _data; private readonly IDialogService _dialogs;
    public ObservableCollection<Payment> Payments {get;}=new();
    public PaymentsViewModel(INavigationService nav,IConnectivityService conn,IAppStateService state,IAppDataService data,IDialogService dialogs):base(nav,conn){_state=state;_data=data;_dialogs=dialogs;}
    public async Task LoadAsync()=>await RunAsync(async()=>{Payments.Clear();var h=_state.CurrentHousehold;if(h is null)return;foreach(var p in await _data.GetPaymentsAsync(h.Id))Payments.Add(p);});
    [RelayCommand] private async Task PayAsync(){await RunAsync(async()=>{var h=_state.CurrentHousehold;if(h is null)return;var p=new Payment{HouseholdId=h.Id,Amount=2000,MonthLabel=DateTime.Now.ToString("MMMM yyyy",new System.Globalization.CultureInfo("fr-FR")),PaidAt=DateTime.Now,Method="Mobile Money",Reference=$"CKW-{DateTime.Now:yyyyMMddHHmmss}",Status=PaymentStatus.Confirmed.ToString()};await _data.AddPaymentAsync(p);await _dialogs.AlertAsync("Paiement effectué","Votre paiement de 2 000 FCFA a été confirmé.");await LoadAsync();});}
}
