using LearningDIHub.CompositionRoot;
using LearningDIHub.Domain.Auditing;
using LearningDIHub.Domain.Circular;
using LearningDIHub.Domain.MessagesSender;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");


//Using Dependency Injection without HostBuilder

var serviceCollection = new ServiceCollection();


IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

serviceCollection
    .AddMessageServices(config)
    .AddAuditingServices();


var serviceProvider = serviceCollection.BuildServiceProvider();

var messageService = serviceProvider.GetRequiredService<MessageService>();
var testController = serviceProvider.GetRequiredService<TestController>();


var a = serviceProvider.GetRequiredService<A>();

var b = serviceProvider.GetRequiredService<B>();

var a2 = serviceProvider.GetRequiredService<A>();


//Old way of doing things without Dependency Injection
//var messageService = new MessageService(new EmailProcessor());

var msg = new Message() { Id = Guid.NewGuid() ,From = "Juan", To = "Dani" , Body = "I Love You"};

System.Console.WriteLine($"Message Service Id: {messageService.Id}");
Console.WriteLine(messageService.SendMessage(msg));

//Uses the lifetime of the services registered for IMessageService,
//if is singleton it will print the same Id for both, if is transient it will print different Ids
Console.WriteLine(testController.Print());

//Testing the lifetimes of the services
Console.WriteLine(a.Print());
Console.WriteLine(b.Print());
Console.WriteLine(a2.Print());

//Putting IDisposable in the classes A and B, didn't dispose the services, because we create the service ourselves
b.Dispose();
a.Dispose();
a2.Dispose();
messageService.Dispose();
testController.Dispose();
//As MessageService is IDisposable and is created by the Builder below, the Builder is responsible for disposing of it.

Console.WriteLine(a.Print()); 

//var builder = Host.CreateApplicationBuilder(args); Create simple Application to register services. 
//Using Dependency Injection with HostBuilder and Default configuration as this will help introduce Validation of Captive Dependencies and Scopes.

var builder = Host.CreateDefaultBuilder(args)
    .UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateOnBuild = true;
        options.ValidateScopes = true; // This will throw an exception if we try to resolve a scoped service from a singleton, which is a common mistake that can lead to memory leaks and other issues.
    })
    .ConfigureServices((hostBuilderContext, services) =>
    {
        //Composition Root 
        services.AddLearningHub(hostBuilderContext.Configuration, config =>
        {
            config.RegisterAsHostedService = true;
        });

    });

using var host = builder.Build();
await host.RunAsync();





var circularServiceCollection = new ServiceCollection();

//Calling extensions of registrations
circularServiceCollection
    .AddLazyCircularRegistration()
    .AddFuncCircularRegistration();


var circularServiceProvider = circularServiceCollection.BuildServiceProvider();

var firstClass = circularServiceProvider.GetRequiredService<FirstClass>();

firstClass.DoIt();

var firstClassFunc = circularServiceProvider.GetRequiredService<FirstClassFunc>();

firstClassFunc.DoIt();






