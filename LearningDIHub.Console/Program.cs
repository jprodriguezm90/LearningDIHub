using LearningDIHub.Console;
using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<MessageService>();
serviceCollection.AddTransient<TestController>();
serviceCollection.AddSingleton<IMessageService,MessageService>();

serviceCollection.AddScoped<ISenderProvider, SMSProcessor>();
serviceCollection.AddScoped<ISenderProvider, EmailProcessor>();

serviceCollection.AddSingleton<A>();

serviceCollection.AddScoped<B>();


var serviceProvider = serviceCollection.BuildServiceProvider();

var messageService = serviceProvider.GetRequiredService<MessageService>();
var testController = serviceProvider.GetRequiredService<TestController>();


var a = serviceProvider.GetRequiredService<A>();

var b = serviceProvider.GetRequiredService<B>();

var a2 = serviceProvider.GetRequiredService<A>();



//var messageService = new MessageService(new EmailProcessor());
var msg = new Message() { Id = Guid.NewGuid() ,From = "Juan", To = "Dani" , Body = "I Love You"};

Console.WriteLine(messageService.SendMessage(msg));

Console.WriteLine(testController.Print());

Console.WriteLine(a.Print());
Console.WriteLine(b.Print());
Console.WriteLine(a2.Print());



var builder = Host.CreateDefaultBuilder(args)
    .UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateOnBuild = true;
        options.ValidateScopes = true;
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<LearningHub>();

        services.AddTransient<IMessageService,MessageService>();

        services.AddScoped<ISenderProvider, EmailProcessor>();
    });

using var host = builder.Build();
await host.RunAsync();


