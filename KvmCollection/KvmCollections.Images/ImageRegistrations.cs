using Autofac;
using KvmCollection.Images.Classifier;
using KvmCollection.Images.Sizer;
using System;
using System.Linq;

namespace KvmCollection.Images;

public class ImageRegistrations : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ImageSizeProcessor>()
            .As<IImageSizeProcessor>()
            .InstancePerDependency();

        builder.RegisterType<ImageClassificationProcessor>()
            .As<IImageClassificationProcessor>()
            .InstancePerDependency();
    }
}
