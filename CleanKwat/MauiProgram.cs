using CleanKwat.Data;
using CleanKwat.Helpers;
using CleanKwat.Services.Implementations;
using CleanKwat.Services.Interfaces;
using CleanKwat.ViewModels;
using Microsoft.Extensions.Logging;

namespace CleanKwat;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AppDatabase>();
        builder.Services.AddSingleton<IAppStateService, AppStateService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<IAppDataService, AppDataService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
        builder.Services.AddSingleton<ISyncService, SyncService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

        builder.Services.AddTransient<OnboardingViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<NeighborhoodSelectionViewModel>();
        builder.Services.AddTransient<HouseholdSetupViewModel>();
        builder.Services.AddTransient<HouseholdHomeViewModel>();
        builder.Services.AddTransient<CalendarViewModel>();
        builder.Services.AddTransient<CollectionDetailViewModel>();
        builder.Services.AddTransient<SubscriptionViewModel>();
        builder.Services.AddTransient<PaymentsViewModel>();
        builder.Services.AddTransient<ReportViewModel>();
        builder.Services.AddTransient<NotificationsViewModel>();
        builder.Services.AddTransient<CollectorLoginViewModel>();
        builder.Services.AddTransient<CollectorDashboardViewModel>();
        builder.Services.AddTransient<CollectorToursViewModel>();
        builder.Services.AddTransient<TourDetailViewModel>();
        builder.Services.AddTransient<ConfirmCollectionViewModel>();
        builder.Services.AddTransient<AdminDashboardViewModel>();
        builder.Services.AddTransient<AdminNeighborhoodsViewModel>();
        builder.Services.AddTransient<AdminCollectionsViewModel>();
        builder.Services.AddTransient<AdminCollectorsViewModel>();

        var app = builder.Build();
        ServiceHelper.Services = app.Services;
        return app;
    }
}
