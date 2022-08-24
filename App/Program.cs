using Domain.AggregateRoots;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services
            .AddTransient<ITriangleRepository, TriangleRepository>();
    })
    .Build();


var repository = host.Services.GetService<ITriangleRepository>();
// Todo: change this to console.readline to get filepath.

try
{

    Console.WriteLine("Enter file path to calculate");
    var importPath = Console.ReadLine();

    Console.WriteLine("Enter file destination");
    var exportPath = Console.ReadLine();

    var triangles = repository.GetAll(importPath);

    var triangleCalculation = new TriangleCalculation(triangles);
    repository.SaveCalculation(exportPath, triangleCalculation);
}
catch (ImportException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    
    foreach (var error in ex.errors)
    {
        Console.WriteLine(error);
    }
}

Console.WriteLine("Press any key to exit");
Console.ReadLine();

static void RunProgram(IServiceProvider services)
{

}