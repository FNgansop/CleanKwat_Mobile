using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.Services.Implementations;

public class AppStateService : IAppStateService
{
    public User? CurrentUser { get; set; }
    public Neighborhood? SelectedNeighborhood { get; set; }
    public Household? CurrentHousehold { get; set; }
}
