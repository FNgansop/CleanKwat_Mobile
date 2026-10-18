using SQLite;

namespace CleanKwat.Models;

public class Neighborhood
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Indexed] public string Name { get; set; } = string.Empty;
    public string CityDistrict { get; set; } = string.Empty;
    public int HouseholdCount { get; set; }
    public int CollectorCount { get; set; }
    public bool IsActive { get; set; } = true;
}
