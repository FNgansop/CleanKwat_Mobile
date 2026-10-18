using CleanKwat.Models;
namespace CleanKwat.Services.Interfaces;
public interface IAppDataService
{
    Task<IReadOnlyList<Neighborhood>> GetNeighborhoodsAsync(string? search = null);
    Task<Neighborhood?> GetNeighborhoodAsync(int id);
    Task<Household?> GetHouseholdByUserAsync(int userId);
    Task<int> SaveHouseholdAsync(Household household);
    Task<Subscription?> GetSubscriptionAsync(int householdId);
    Task<IReadOnlyList<CollectionSchedule>> GetCollectionsAsync(int householdId);
    Task<CollectionSchedule?> GetCollectionAsync(int id);
    Task<IReadOnlyList<Payment>> GetPaymentsAsync(int householdId);
    Task<int> AddPaymentAsync(Payment payment);
    Task<IReadOnlyList<Report>> GetReportsAsync(int householdId);
    Task<int> AddReportAsync(Report report);
    Task<IReadOnlyList<AppNotification>> GetNotificationsAsync(int userId);
    Task<IReadOnlyList<Tour>> GetToursForCollectorAsync(int collectorUserId);
    Task<Tour?> GetTourAsync(int id);
    Task<IReadOnlyList<TourStop>> GetTourStopsAsync(int tourId);
    Task UpdateTourStopAsync(TourStop stop);
    Task<IReadOnlyList<Neighborhood>> GetAdminNeighborhoodsAsync();
    Task<int> AddNeighborhoodAsync(Neighborhood neighborhood);
    Task UpdateNeighborhoodAsync(Neighborhood neighborhood);
    Task<IReadOnlyList<Tour>> GetAllToursAsync();
    Task<int> AddTourAsync(Tour tour);
    Task<IReadOnlyList<User>> GetCollectorsAsync();
}
