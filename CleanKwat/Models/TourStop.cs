using SQLite;

namespace CleanKwat.Models;

public class TourStop
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Indexed] public int TourId { get; set; }
    public string HouseholdName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsCollected { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool PendingSync { get; set; }
}
