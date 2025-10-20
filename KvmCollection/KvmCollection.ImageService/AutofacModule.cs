using Autofac;
using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollections.XpoRepository;

namespace KvmCollection.ImageService;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<XpoCoverRepository>()
            .As<IDataRepository<CoverArtDto>>()
            .InstancePerDependency();
        
        builder.RegisterType<ImageProcessor>()
            .As<IImageProcessor>()
            .InstancePerDependency();
    }
}