using CleanKwat.Models;
namespace CleanKwat.Services.Interfaces;
public interface IAppStateService
{
    User? CurrentUser { get; set; }
    Neighborhood? SelectedNeighborhood { get; set; }
    Household? CurrentHousehold { get; set; }
}
