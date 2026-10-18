namespace CleanKwat.Services.Interfaces;
public interface ISyncService
{
    DateTime? LastSync { get; }
    Task<bool> SyncAsync();
}
