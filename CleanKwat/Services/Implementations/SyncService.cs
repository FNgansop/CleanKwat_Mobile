using CleanKwat.Services.Interfaces;

namespace CleanKwat.Services.Implementations;

public class SyncService : ISyncService
{
    private readonly IConnectivityService _connectivity;
    public SyncService(IConnectivityService connectivity) => _connectivity = connectivity;
    public DateTime? LastSync
    {
        get { var raw = Preferences.Default.Get("last_sync", string.Empty); return DateTime.TryParse(raw, out var value) ? value : null; }
    }
    public async Task<bool> SyncAsync()
    {
        if (!_connectivity.IsOnline) return false;
        await Task.Delay(350);
        Preferences.Default.Set("last_sync", DateTime.Now.ToString("O"));
        return true;
    }
}
