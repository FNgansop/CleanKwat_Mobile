using SQLite;

namespace CleanKwat.Models;

public class Subscription
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Indexed] public int HouseholdId { get; set; }
    public string PlanName { get; set; } = "Standard";
    public int MonthlyAmount { get; set; } = 2000;
    public string CollectorName { get; set; } = "EcoNet Services";
    public DateTime StartedAt { get; set; } = new(2026, 10, 1);
    public bool IsActive { get; set; } = true;
}
