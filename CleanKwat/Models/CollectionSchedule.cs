using SQLite;

namespace CleanKwat.Models;

public class CollectionSchedule
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public int HouseholdId { get; set; }
    public int NeighborhoodId { get; set; }
    public string WasteType { get; set; } = "Ordures ménagères";
    public DateTime Date { get; set; }
    public string TimeWindow { get; set; } = "06 h – 10 h";
    public string Zone { get; set; } = string.Empty;
    public string CollectorName { get; set; } = string.Empty;
    public string Status { get; set; } = CollectionStatus.Planned.ToString();
    public string Instructions { get; set; } = "Sortez vos sacs la veille après 20 h et placez-les devant votre habitation.";
    public bool PendingSync { get; set; }
    [Ignore] public string StatusDisplay => Status switch
    {
        nameof(CollectionStatus.Upcoming) => "À venir",
        nameof(CollectionStatus.Planned) => "Planifiée",
        nameof(CollectionStatus.InProgress) => "En cours",
        nameof(CollectionStatus.Completed) => "Effectuée",
        nameof(CollectionStatus.Missed) => "Passage manqué",
        nameof(CollectionStatus.Delayed) => "En retard",
        _ => Status
    };
}
