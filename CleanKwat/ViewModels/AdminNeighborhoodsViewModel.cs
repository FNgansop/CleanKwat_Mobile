using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.ViewModels;

public partial class AdminNeighborhoodsViewModel : BaseViewModel
{
    private readonly IAppDataService _data;
    private readonly IDialogService _dialogs;
    private IReadOnlyList<Neighborhood> _allNeighborhoods = Array.Empty<Neighborhood>();

    public ObservableCollection<Neighborhood> Neighborhoods { get; } = new();

    [ObservableProperty] private string searchText = string.Empty;
    [ObservableProperty] private string selectedFilter = "Tous";

    public int AllCount => _allNeighborhoods.Count;
    public int ActiveCount => _allNeighborhoods.Count(x => x.IsActive);
    public int InactiveCount => _allNeighborhoods.Count(x => !x.IsActive);

    public AdminNeighborhoodsViewModel(
        INavigationService nav,
        IConnectivityService conn,
        IAppDataService data,
        IDialogService dialogs) : base(nav, conn)
    {
        _data = data;
        _dialogs = dialogs;
    }

    public async Task LoadAsync() => await RunAsync(async () =>
    {
        _allNeighborhoods = await _data.GetAdminNeighborhoodsAsync();
        RefreshVisibleItems();
        OnPropertyChanged(nameof(AllCount));
        OnPropertyChanged(nameof(ActiveCount));
        OnPropertyChanged(nameof(InactiveCount));
    });

    partial void OnSearchTextChanged(string value) => RefreshVisibleItems();

    [RelayCommand]
    private void Filter(string filter)
    {
        SelectedFilter = filter;
        RefreshVisibleItems();
    }

    private void RefreshVisibleItems()
    {
        IEnumerable<Neighborhood> query = _allNeighborhoods;

        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(x => x.Name.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));

        query = SelectedFilter switch
        {
            "Actifs" => query.Where(x => x.IsActive),
            "Inactifs" => query.Where(x => !x.IsActive),
            _ => query
        };

        Neighborhoods.Clear();
        foreach (var n in query.Take(50)) Neighborhoods.Add(n);
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        var name = await _dialogs.PromptAsync("Ajouter un quartier", "Nom du quartier", placeholder: "Ex. Deido");
        if (string.IsNullOrWhiteSpace(name)) return;
        await _data.AddNeighborhoodAsync(new Neighborhood { Name = name.Trim(), CityDistrict = "Douala", IsActive = true });
        await LoadAsync();
    }

    [RelayCommand]
    private async Task EditAsync(Neighborhood n)
    {
        var name = await _dialogs.PromptAsync("Modifier le quartier", "Nouveau nom", placeholder: n.Name);
        if (string.IsNullOrWhiteSpace(name)) return;
        n.Name = name.Trim();
        await _data.UpdateNeighborhoodAsync(n);
        await LoadAsync();
    }
}
