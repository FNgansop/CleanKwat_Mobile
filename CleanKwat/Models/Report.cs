using SQLite;

namespace CleanKwat.Models;

public class Report
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public int HouseholdId { get; set; }
    public int CollectionScheduleId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = ReportStatus.Open.ToString();
    public bool PendingSync { get; set; }
    [Ignore] public string StatusDisplay => Status switch
    {
        nameof(ReportStatus.Open) => "En attente",
        nameof(ReportStatus.InProgress) => "En cours",
        nameof(ReportStatus.Resolved) => "Résolu",
        _ => Status
    };
}
