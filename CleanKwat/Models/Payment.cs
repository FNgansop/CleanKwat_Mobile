using SQLite;

namespace CleanKwat.Models;

public class Payment
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public int HouseholdId { get; set; }
    public int Amount { get; set; }
    public string MonthLabel { get; set; } = string.Empty;
    public DateTime PaidAt { get; set; }
    public string Method { get; set; } = "Mobile Money";
    public string Reference { get; set; } = string.Empty;
    public string Status { get; set; } = PaymentStatus.Confirmed.ToString();
}
