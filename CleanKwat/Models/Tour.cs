using SQLite;

namespace CleanKwat.Models;

public class Tour
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public int CollectorUserId { get; set; }
    public int NeighborhoodId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string TimeWindow { get; set; } = string.Empty;
    public int PlannedCount { get; set; }
    public int CompletedCount { get; set; }
    public string Status { get; set; } = TourStatus.Planned.ToString();
    [Ignore] public double Progress => PlannedCount == 0 ? 0 : (double)CompletedCount / PlannedCount;
    [Ignore] public int ProgressPercent => (int)Math.Round(Progress * 100);
    [Ignore] public string StatusDisplay => Status switch
    {
        nameof(TourStatus.InProgress) => "En cours",
        nameof(TourStatus.Completed) => "Terminée",
        nameof(TourStatus.Missed) => "Non réalisée",
        _ => "À venir"
    };
}
