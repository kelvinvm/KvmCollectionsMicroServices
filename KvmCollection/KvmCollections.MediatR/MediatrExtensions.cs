using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace KvmCollection.MediatR;

public static class MediatrExtensions
{
    private static string MediatRLicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyMzY4MDAwIiwiaWF0IjoiMTc2MDg5NzczMSIsImFjY291bnRfaWQiOiIwMTk4YmEwNmY1MzA3OWU4OGU1ODg1MGUxNmY2ZmZmMiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazJ4MGYxcTZmZTlqOTYyNGp4dDY3anQyIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.VmbnKeX2E-ntMcwbBj0G37CveTgPX6OzcdINw6pvLVYiXTKYBs7DQccJvAYx5BLIGMT2ZLH_SrVqJdOWL5o1J94HJ4fY9yuOWDy1jgVLX1ID0hQmma76lrmMvikCZlmK6Xjr9Wvwt40PsCzIiKQjCQjcoihlFBBJo8-6ghmt01vGOq-LmHWmYF5W7niH6AdAiA1NYQwjtT-SYmKMiQ-LMDTvnzjB3YWNqEUT3AHX-j5ESCxeNlF1UiDGvPiFtwooPVG6Ue6zQbKtIxu4Le6k_1gSoCfSKFEbTvuXEO2rkW4v-13az1B-ntLw7Fwl9KKJUvwvk0FNh--zgdPT1U6oPQ";

    public static void AddMediatRDependencies(this ContainerBuilder builder, Assembly[] assemblies)
    {
        var mediatrConfig = MediatRConfigurationBuilder
            .Create(MediatRLicenseKey, assemblies)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

        builder.RegisterMediatR(mediatrConfig);
    }
}