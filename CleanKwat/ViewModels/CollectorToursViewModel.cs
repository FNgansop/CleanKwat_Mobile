using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Constants;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class CollectorToursViewModel : BaseViewModel
{
    private readonly IAppStateService _state;
    private readonly IAppDataService _data;
    private IReadOnlyList<Tour> _allTours = Array.Empty<Tour>();

    public ObservableCollection<Tour> Tours { get; } = new();
    [ObservableProperty] private string selectedPeriod = "Aujourd’hui";
    [ObservableProperty] private string periodTitle = "Mercredi 15 mai 2024";

    public string TourCountLabel => $"{Tours.Count} tournée{(Tours.Count > 1 ? "s" : string.Empty)}";

    public CollectorToursViewModel(INavigationService nav, IConnectivityService conn, IAppStateService state, IAppDataService data) : base(nav, conn)
    {
        _state = state;
        _data = data;
    }

    public async Task LoadAsync() => await RunAsync(async () =>
    {
        var uid = _state.CurrentUser?.Id ?? 2;
        _allTours = await _data.GetToursForCollectorAsync(uid);
        ApplyPeriod();
    });

    [RelayCommand]
    private void SelectPeriod(string period)
    {
        SelectedPeriod = period;
        ApplyPeriod();
    }

    private void ApplyPeriod()
    {
        // Les données de démonstration correspondent à la date de la maquette officielle.
        var reference = new DateTime(2024, 5, 15);
        IEnumerable<Tour> query = SelectedPeriod switch
        {
            "Demain" => _allTours.Where(x => x.Date.Date == reference.AddDays(1)),
            "Cette semaine" => _allTours.Where(x => x.Date.Date >= reference && x.Date.Date < reference.AddDays(7)),
            _ => _allTours.Where(x => x.Date.Date == reference)
        };

        PeriodTitle = SelectedPeriod switch
        {
            "Demain" => "Jeudi 16 mai 2024",
            "Cette semaine" => "Semaine du 15 mai 2024",
            _ => "Mercredi 15 mai 2024"
        };

        Tours.Clear();
        foreach (var t in query.Take(10)) Tours.Add(t);
        OnPropertyChanged(nameof(TourCountLabel));
    }

    [RelayCommand]
    private Task Open(Tour t) => Navigation.GoToAsync($"{Routes.TourDetail}?id={t.Id}");
}
