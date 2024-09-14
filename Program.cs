using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerlessCRUDApi;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        var connString = Environment.GetEnvironmentVariable("SqlServerConnection");
        services.AddDbContext<ShopContext>(opts => opts.UseSqlServer(connString));
    })
    .Build();

host.Run();
