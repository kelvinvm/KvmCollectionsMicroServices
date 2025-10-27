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

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Your Angular dev server
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

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

// Use CORS before other middleware
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Enable static files to serve images
app.UseStaticFiles();
app.MapStaticAssets().ShortCircuit();

app.Run();