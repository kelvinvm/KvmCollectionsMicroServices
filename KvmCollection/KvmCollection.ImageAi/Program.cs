using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using KvmCollection.ImageAi;
using KvmCollection.Images;
using KvmCollection.MediatR;

string connectionString = $"{MSSqlConnectionProvider.GetConnectionString("WIN11-DEV", "Images")};TrustServerCertificate=true";
XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var autofacModule = new AutofacModule(new[]
{
    // These are the assemblies that contain MediatR handlers and related classes
    typeof(MediatrExtensions).Assembly,
    typeof(ImageRegistrations).Assembly,
});

builder.Host.ConfigureContainer<ContainerBuilder>(bldr => bldr.RegisterModule(autofacModule));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
