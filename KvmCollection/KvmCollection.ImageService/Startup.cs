using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using KvmCollection.ImageService;

public static class Startup
{
    public static WebApplication ConfigureWebApplication(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(bldr => bldr.RegisterModule<AutofacModule>());

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddLogging();

        var app = builder.Build();

        // Setup the XPO Data Layer; currently SqlServer, likely MySql in prod
        ConfigureXpoDataLayer();

        return app;
    }

    private static void ConfigureXpoDataLayer()
    {
        string connectionString = $"{MSSqlConnectionProvider.GetConnectionString("WIN11-DEV", "ImageService")};TrustServerCertificate=true";
        XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
    }
}
