using SQLite;

namespace CleanKwat.Models;

public class Household
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Indexed] public int UserId { get; set; }
    public int NeighborhoodId { get; set; }
    public int HouseholdSize { get; set; } = 4;
    public string HousingType { get; set; } = "Maison individuelle";
    public string Address { get; set; } = string.Empty;
}
