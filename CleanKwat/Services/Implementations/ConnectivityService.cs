using CleanKwat.Services.Interfaces;
using Microsoft.Maui.Networking;

namespace CleanKwat.Services.Implementations;

public class ConnectivityService : IConnectivityService
{
    public bool IsOnline => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
}
