using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollection.ImageService;
using Microsoft.AspNetCore.Mvc;

var app = Startup.ConfigureWebApplication(args);

app.MapGet("/", async (IImageProcessor processor) 
    => await processor.GetAvailableCoverArtAsync())
    .DisableAntiforgery();

app.MapPost("/", async (IImageProcessor processor, IFormFile file, string name)
    => await processor.CreateCoverArtAsync(name, file.GetBytes()))
    .DisableAntiforgery()
    .Accepts<IFormFile>("multipart/form-data");

app.Run();


