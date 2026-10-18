namespace CleanKwat.Models;

public class CalendarDayItem
{
    public int Day { get; set; }
    public bool IsEmpty { get; set; }
    public bool HasCollection { get; set; }
    public bool IsSelected { get; set; }
    public string DayText => IsEmpty ? string.Empty : Day.ToString();
}
