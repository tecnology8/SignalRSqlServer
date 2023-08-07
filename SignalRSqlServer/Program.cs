using SignalRSqlServer.Hubs;
using SignalRSqlServer.MiddlewareExtensions;
using SignalRSqlServer.SuscribeTableDependencies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

//Dependency Injection
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();
builder.Services.AddSingleton<SubscribeCustomerTableDependency>();

var app = builder.Build();
var connectionStrings = app.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");


app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionStrings);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(connectionStrings);
app.UseSqlTableDependency<SubscribeCustomerTableDependency>(connectionStrings);

app.Run();
