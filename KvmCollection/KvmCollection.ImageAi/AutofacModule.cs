using Autofac;
using KvmCollection.Images;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System.Reflection;
using KvmCollection.MediatR;
using KvmCollections.XpoRepository;

namespace KvmCollection.ImageAi;

public class AutofacModule : Autofac.Module
{
    private readonly Assembly[] _assemblies;

    public AutofacModule(Assembly[] mediatrAssemblies)
    {
        _assemblies = mediatrAssemblies;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyModules(
            typeof(RepositoryRegistrations).Assembly,
            typeof(ImageRegistrations).Assembly);

        builder.RegisterType<ImageManager>()
            .As<IImageManager>()
            .InstancePerDependency();

        builder.RegisterType<AlbumManager>()
            .As<IAlbumManager>()
            .InstancePerDependency();

        builder.RegisterType<WebImageManager>()
            .As<IWebImageManager>()
            .InstancePerDependency();

        builder.AddMediatRDependencies(_assemblies);

        builder.Register<ComputerVisionClient>(ctx =>
        {
            string key = Environment.GetEnvironmentVariable("VISION_KEY") ?? "";
            string endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT") ?? "";
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
            return client;
        }).As<IComputerVisionClient>().SingleInstance();
    }
}
