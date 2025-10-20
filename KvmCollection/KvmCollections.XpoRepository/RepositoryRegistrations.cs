using Autofac;
using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using System.Linq;

namespace KvmCollections.XpoRepository;


public class RepositoryRegistrations : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ImageClassificationRepository>()
            .As<IDataRepository<ImageClassification>>()
            .InstancePerDependency();
    }
}