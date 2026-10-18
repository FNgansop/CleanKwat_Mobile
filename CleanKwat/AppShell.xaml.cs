using CleanKwat.Constants;
using CleanKwat.Views.Admin;
using CleanKwat.Views.Auth;
using CleanKwat.Views.Collector;
using CleanKwat.Views.Household;

namespace CleanKwat;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(Routes.Register, typeof(RegisterPage));
        Routing.RegisterRoute(Routes.Login, typeof(LoginPage));
        Routing.RegisterRoute(Routes.Neighborhood, typeof(NeighborhoodSelectionPage));
        Routing.RegisterRoute(Routes.HouseholdSetup, typeof(HouseholdSetupPage));
        Routing.RegisterRoute(Routes.HouseholdHome, typeof(HouseholdHomePage));
        Routing.RegisterRoute(Routes.Calendar, typeof(CalendarPage));
        Routing.RegisterRoute(Routes.CollectionDetail, typeof(CollectionDetailPage));
        Routing.RegisterRoute(Routes.Subscription, typeof(SubscriptionPage));
        Routing.RegisterRoute(Routes.Payments, typeof(PaymentsPage));
        Routing.RegisterRoute(Routes.Report, typeof(ReportPage));
        Routing.RegisterRoute(Routes.Notifications, typeof(NotificationsPage));
        Routing.RegisterRoute(Routes.CollectorLogin, typeof(CollectorLoginPage));
        Routing.RegisterRoute(Routes.CollectorDashboard, typeof(CollectorDashboardPage));
        Routing.RegisterRoute(Routes.CollectorTours, typeof(CollectorToursPage));
        Routing.RegisterRoute(Routes.TourDetail, typeof(TourDetailPage));
        Routing.RegisterRoute(Routes.ConfirmCollection, typeof(ConfirmCollectionPage));
        Routing.RegisterRoute(Routes.AdminDashboard, typeof(AdminDashboardPage));
        Routing.RegisterRoute(Routes.AdminNeighborhoods, typeof(AdminNeighborhoodsPage));
        Routing.RegisterRoute(Routes.AdminCollections, typeof(AdminCollectionsPage));
        Routing.RegisterRoute(Routes.AdminCollectors, typeof(AdminCollectorsPage));
    }
}
