using CleanKwat.Helpers;
using CleanKwat.Models;
using SQLite;

namespace CleanKwat.Data;

public class AppDatabase
{
    private SQLiteAsyncConnection? _db;
    private readonly SemaphoreSlim _gate = new(1,1);
    private bool _initialized;

    private async Task<SQLiteAsyncConnection> DbAsync()
    {
        if (_initialized && _db is not null) return _db;
        await _gate.WaitAsync();
        try
        {
            if (_initialized && _db is not null) return _db;
            var path = Path.Combine(FileSystem.AppDataDirectory, "cleankwat.db3");
            _db = new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _db.CreateTableAsync<User>();
            await _db.CreateTableAsync<Neighborhood>();
            await _db.CreateTableAsync<Household>();
            await _db.CreateTableAsync<Subscription>();
            await _db.CreateTableAsync<CollectionSchedule>();
            await _db.CreateTableAsync<Tour>();
            await _db.CreateTableAsync<TourStop>();
            await _db.CreateTableAsync<Payment>();
            await _db.CreateTableAsync<Report>();
            await _db.CreateTableAsync<AppNotification>();
            await SeedAsync(_db);
            _initialized = true;
            return _db;
        }
        finally { _gate.Release(); }
    }

    private static async Task SeedAsync(SQLiteAsyncConnection db)
    {
        if (await db.Table<User>().CountAsync() > 0) return;
        var users = new[]
        {
            new User{Identifier="+237677777777",DisplayName="Fredi",PasswordHash=PasswordHasher.Hash("Clean123!"),Role=UserRole.Household.ToString()},
            new User{Identifier="collector@cleankwat.local",DisplayName="Joseph",PasswordHash=PasswordHasher.Hash("Collector123!"),Role=UserRole.Collector.ToString()},
            new User{Identifier="admin@cleankwat.local",DisplayName="Admin",PasswordHash=PasswordHasher.Hash("Admin123!"),Role=UserRole.Administrator.ToString()}
        };
        foreach (var u in users) await db.InsertAsync(u);
        var neighborhoods = new[]
        {
            new Neighborhood{Name="Bonanjo",CityDistrict="Douala 1er",HouseholdCount=120,CollectorCount=3},
            new Neighborhood{Name="Akwa",CityDistrict="Douala 1er",HouseholdCount=150,CollectorCount=4},
            new Neighborhood{Name="Bali",CityDistrict="Douala 1er",HouseholdCount=95,CollectorCount=2},
            new Neighborhood{Name="Bépanda",CityDistrict="Douala 3e",HouseholdCount=110,CollectorCount=3},
            new Neighborhood{Name="Makepe",CityDistrict="Douala 5e",HouseholdCount=98,CollectorCount=3},
            new Neighborhood{Name="Ngousso",CityDistrict="Yaoundé 1er",HouseholdCount=120,CollectorCount=3},
            new Neighborhood{Name="Briqueterie",CityDistrict="Yaoundé 2e",HouseholdCount=95,CollectorCount=2},
            new Neighborhood{Name="Mvog-Mbi",CityDistrict="Yaoundé 4e",HouseholdCount=110,CollectorCount=3},
            new Neighborhood{Name="Nkolbisson",CityDistrict="Yaoundé 7e",HouseholdCount=80,CollectorCount=2},
            new Neighborhood{Name="Ekounou",CityDistrict="Yaoundé 4e",HouseholdCount=100,CollectorCount=3},
            new Neighborhood{Name="Essos",CityDistrict="Yaoundé 5e",HouseholdCount=130,CollectorCount=4},
            new Neighborhood{Name="Mvog-Ada",CityDistrict="Yaoundé 5e",HouseholdCount=70,CollectorCount=2}
        };
        foreach (var n in neighborhoods) await db.InsertAsync(n);
        var bonanjo = neighborhoods[0];
        var ngousso = neighborhoods[5];
        var household = new Household{UserId=users[0].Id,NeighborhoodId=bonanjo.Id,HouseholdSize=4,HousingType="Maison individuelle"};
        await db.InsertAsync(household);
        await db.InsertAsync(new Subscription{HouseholdId=household.Id,PlanName="Standard",MonthlyAmount=2000,CollectorName="EcoNet Services",StartedAt=new DateTime(2026,10,1),IsActive=true});
        var dates = new[]{new DateTime(2024,5,15),new DateTime(2024,5,22),new DateTime(2024,5,29)};
        for(int i=0;i<dates.Length;i++) await db.InsertAsync(new CollectionSchedule{HouseholdId=household.Id,NeighborhoodId=ngousso.Id,Date=dates[i],TimeWindow="06 h – 10 h",Zone="Ngousso – Zone 1 · Yaoundé",CollectorName="Équipe Ngousso",Status=i==0?CollectionStatus.Upcoming.ToString():CollectionStatus.Planned.ToString()});
        await db.InsertAsync(new CollectionSchedule{HouseholdId=household.Id,NeighborhoodId=bonanjo.Id,Date=new DateTime(2026,10,9),TimeWindow="06H – 10H",Zone="Bonanjo",CollectorName="EcoNet Services",Status=CollectionStatus.Upcoming.ToString()});
        foreach(var p in new[]{
            new Payment{HouseholdId=household.Id,Amount=2000,MonthLabel="Octobre 2026",PaidAt=new DateTime(2026,10,2),Method="Mobile Money",Reference="CKW-202610-001"},
            new Payment{HouseholdId=household.Id,Amount=2000,MonthLabel="Septembre 2026",PaidAt=new DateTime(2026,9,3),Method="Mobile Money",Reference="CKW-202609-001"}}) await db.InsertAsync(p);
        await db.InsertAsync(new AppNotification{UserId=users[0].Id,Title="Collecte demain",Message="La collecte est prévue entre 06h et 10h.",CreatedAt=DateTime.Now.AddHours(-2),Type="Collection"});
        var tourData = new[]{
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=ngousso.Id,Name="Ngousso – Zone 1",Date=new DateTime(2024,5,15),TimeWindow="08 h – 10 h",PlannedCount=12,CompletedCount=8,Status=TourStatus.InProgress.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=ngousso.Id,Name="Ngousso – Zone 2",Date=new DateTime(2024,5,15),TimeWindow="10 h – 12 h",PlannedCount=6,CompletedCount=0,Status=TourStatus.Planned.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=neighborhoods[8].Id,Name="Carrefour Nkolbisson",Date=new DateTime(2024,5,15),TimeWindow="12 h – 14 h",PlannedCount=4,CompletedCount=0,Status=TourStatus.Planned.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=neighborhoods[2].Id,Name="Bali – Zone 2",Date=new DateTime(2024,5,15),TimeWindow="08 h – 12 h",PlannedCount=8,CompletedCount=8,Status=TourStatus.Completed.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=neighborhoods[7].Id,Name="Mvog-Mbi – Zone 1",Date=new DateTime(2024,5,14),TimeWindow="08 h – 12 h",PlannedCount=10,CompletedCount=10,Status=TourStatus.Completed.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=neighborhoods[8].Id,Name="Nkolbisson – Zone 1",Date=new DateTime(2024,5,13),TimeWindow="08 h – 12 h",PlannedCount=7,CompletedCount=0,Status=TourStatus.Missed.ToString()},
            new Tour{CollectorUserId=users[1].Id,NeighborhoodId=neighborhoods[10].Id,Name="Essos – Zone 1",Date=new DateTime(2024,5,12),TimeWindow="08 h – 12 h",PlannedCount=9,CompletedCount=9,Status=TourStatus.Completed.ToString()}
        };
        foreach (var t in tourData) await db.InsertAsync(t);
        var names = new[]{"Mme Ndongo","M. Essomba","Mme Tala","M. Kamga","Mme Aline","M. Nguetso","Mme Mballa","M. Fopa","Mme Nnomo","M. Etoa","Mme Essono","M. Tchoua"};
        for(int i=0;i<names.Length;i++) await db.InsertAsync(new TourStop{TourId=tourData[0].Id,HouseholdName=names[i],Address=$"Ngousso, domicile {i+1}",IsCollected=i<8,Notes=i==2?"Sortir les sacs devant la porte.":string.Empty});
    }

    public async Task<User?> FindUserAsync(string identifier) => await (await DbAsync()).Table<User>().Where(x=>x.Identifier==identifier).FirstOrDefaultAsync();
    public async Task<User?> GetUserAsync(int id) => await (await DbAsync()).FindAsync<User>(id);
    public async Task<int> InsertUserAsync(User user) => await (await DbAsync()).InsertAsync(user);
    public async Task<List<Neighborhood>> NeighborhoodsAsync() => await (await DbAsync()).Table<Neighborhood>().OrderBy(x=>x.Name).ToListAsync();
    public async Task<Neighborhood?> NeighborhoodAsync(int id) => await (await DbAsync()).FindAsync<Neighborhood>(id);
    public async Task<Household?> HouseholdByUserAsync(int userId) => await (await DbAsync()).Table<Household>().Where(x=>x.UserId==userId).FirstOrDefaultAsync();
    public async Task<int> SaveHouseholdAsync(Household h)
    {
        var db = await DbAsync();
        if (h.Id != 0) return await db.UpdateAsync(h);
        var result = await db.InsertAsync(h);
        await db.InsertAsync(new Subscription{HouseholdId=h.Id,PlanName="Standard",MonthlyAmount=2000,CollectorName="EcoNet Services",StartedAt=DateTime.Today,IsActive=true});
        await db.InsertAsync(new CollectionSchedule{HouseholdId=h.Id,NeighborhoodId=h.NeighborhoodId,Date=DateTime.Today.AddDays(2),TimeWindow="06H – 10H",Zone="Quartier sélectionné",CollectorName="EcoNet Services",Status=CollectionStatus.Upcoming.ToString()});
        return result;
    }
    public async Task<Subscription?> SubscriptionAsync(int householdId) => await (await DbAsync()).Table<Subscription>().Where(x=>x.HouseholdId==householdId).FirstOrDefaultAsync();
    public async Task<List<CollectionSchedule>> CollectionsAsync(int householdId) => await (await DbAsync()).Table<CollectionSchedule>().Where(x=>x.HouseholdId==householdId).OrderBy(x=>x.Date).ToListAsync();
    public async Task<CollectionSchedule?> CollectionAsync(int id) => await (await DbAsync()).FindAsync<CollectionSchedule>(id);
    public async Task<List<Payment>> PaymentsAsync(int householdId) => await (await DbAsync()).Table<Payment>().Where(x=>x.HouseholdId==householdId).OrderByDescending(x=>x.PaidAt).ToListAsync();
    public async Task<int> InsertPaymentAsync(Payment p) => await (await DbAsync()).InsertAsync(p);
    public async Task<List<Report>> ReportsAsync(int householdId) => await (await DbAsync()).Table<Report>().Where(x=>x.HouseholdId==householdId).OrderByDescending(x=>x.CreatedAt).ToListAsync();
    public async Task<int> InsertReportAsync(Report r) => await (await DbAsync()).InsertAsync(r);
    public async Task<List<AppNotification>> NotificationsAsync(int userId) => await (await DbAsync()).Table<AppNotification>().Where(x=>x.UserId==userId).OrderByDescending(x=>x.CreatedAt).ToListAsync();
    public async Task<List<Tour>> ToursByCollectorAsync(int userId) => await (await DbAsync()).Table<Tour>().Where(x=>x.CollectorUserId==userId).OrderBy(x=>x.Date).ToListAsync();
    public async Task<List<Tour>> AllToursAsync() => await (await DbAsync()).Table<Tour>().OrderByDescending(x=>x.Date).ToListAsync();
    public async Task<Tour?> TourAsync(int id) => await (await DbAsync()).FindAsync<Tour>(id);
    public async Task<List<TourStop>> TourStopsAsync(int tourId) => await (await DbAsync()).Table<TourStop>().Where(x=>x.TourId==tourId).ToListAsync();
    public async Task UpdateTourStopAsync(TourStop stop) => await (await DbAsync()).UpdateAsync(stop);
    public async Task<int> InsertNeighborhoodAsync(Neighborhood n) => await (await DbAsync()).InsertAsync(n);
    public async Task UpdateNeighborhoodAsync(Neighborhood n) => await (await DbAsync()).UpdateAsync(n);
    public async Task<int> InsertTourAsync(Tour t) => await (await DbAsync()).InsertAsync(t);
    public async Task<List<User>> CollectorsAsync() => await (await DbAsync()).Table<User>().Where(x=>x.Role=="Collector").ToListAsync();
}
