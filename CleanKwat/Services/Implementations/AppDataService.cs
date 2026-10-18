using CleanKwat.Data;
using CleanKwat.Models;
using CleanKwat.Services.Interfaces;

namespace CleanKwat.Services.Implementations;

public class AppDataService : IAppDataService
{
    private readonly AppDatabase _db;
    public AppDataService(AppDatabase db) => _db = db;
    public async Task<IReadOnlyList<Neighborhood>> GetNeighborhoodsAsync(string? search = null)
    {
        var data = await _db.NeighborhoodsAsync();
        if (string.IsNullOrWhiteSpace(search)) return data;
        return data.Where(x => x.Name.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase) || x.CityDistrict.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
    }
    public Task<Neighborhood?> GetNeighborhoodAsync(int id) => _db.NeighborhoodAsync(id);
    public Task<Household?> GetHouseholdByUserAsync(int userId) => _db.HouseholdByUserAsync(userId);
    public Task<int> SaveHouseholdAsync(Household household) => _db.SaveHouseholdAsync(household);
    public Task<Subscription?> GetSubscriptionAsync(int householdId) => _db.SubscriptionAsync(householdId);
    public async Task<IReadOnlyList<CollectionSchedule>> GetCollectionsAsync(int householdId) => await _db.CollectionsAsync(householdId);
    public Task<CollectionSchedule?> GetCollectionAsync(int id) => _db.CollectionAsync(id);
    public async Task<IReadOnlyList<Payment>> GetPaymentsAsync(int householdId) => await _db.PaymentsAsync(householdId);
    public Task<int> AddPaymentAsync(Payment payment) => _db.InsertPaymentAsync(payment);
    public async Task<IReadOnlyList<Report>> GetReportsAsync(int householdId) => await _db.ReportsAsync(householdId);
    public Task<int> AddReportAsync(Report report) => _db.InsertReportAsync(report);
    public async Task<IReadOnlyList<AppNotification>> GetNotificationsAsync(int userId) => await _db.NotificationsAsync(userId);
    public async Task<IReadOnlyList<Tour>> GetToursForCollectorAsync(int collectorUserId) => await _db.ToursByCollectorAsync(collectorUserId);
    public Task<Tour?> GetTourAsync(int id) => _db.TourAsync(id);
    public async Task<IReadOnlyList<TourStop>> GetTourStopsAsync(int tourId) => await _db.TourStopsAsync(tourId);
    public Task UpdateTourStopAsync(TourStop stop) => _db.UpdateTourStopAsync(stop);
    public async Task<IReadOnlyList<Neighborhood>> GetAdminNeighborhoodsAsync() => await _db.NeighborhoodsAsync();
    public Task<int> AddNeighborhoodAsync(Neighborhood neighborhood) => _db.InsertNeighborhoodAsync(neighborhood);
    public Task UpdateNeighborhoodAsync(Neighborhood neighborhood) => _db.UpdateNeighborhoodAsync(neighborhood);
    public async Task<IReadOnlyList<Tour>> GetAllToursAsync() => await _db.AllToursAsync();
    public Task<int> AddTourAsync(Tour tour) => _db.InsertTourAsync(tour);
    public async Task<IReadOnlyList<User>> GetCollectorsAsync() => await _db.CollectorsAsync();
}
