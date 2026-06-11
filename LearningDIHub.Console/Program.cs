using LearningDIHub.Console;
using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");


//Using Dependency Injection without HostBuilder

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<MessageService>();
serviceCollection.AddTransient<TestController>();
serviceCollection.AddSingleton<IMessageService,MessageService>();

//Testing multiple implementations of the same interface
serviceCollection.AddScoped<ISenderProvider, SMSProcessor>();
serviceCollection.AddScoped<ISenderProvider, EmailProcessor>();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

serviceCollection.AddOptions<ServiceSelector>().Bind(config.GetSection(ServiceSelector.SectionName));


serviceCollection.AddTransient<A>();
serviceCollection.AddScoped<B>();


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
        services.AddHostedService<LearningHub>();

        //Note. you can't rely on ValidateScopes, because if you register a service as singleton,
        //it will be resolved as singleton even if it has a scoped dependency,
        //and this will not throw an exception until you try to resolve the service, which can lead to runtime errors.
        //So it's important to be careful when registering services and to avoid registering services with incompatible lifetimes.
        services.AddTransient<IMessageService,MessageService>();

        services.AddScoped<ISenderProvider, EmailProcessor>();

        services.AddOptions<ServiceSelector>().Bind(hostBuilderContext.Configuration.GetSection(ServiceSelector.SectionName));

        services.AddHttpClient("message", options =>
        {
            options.BaseAddress = new Uri("https://raw.githubusercontent.com/jprodriguezm90/LearningDIHub/refs/heads/master/");
        });
        services.AddTransient<IHttpMessageSource, HttpMessageSource>();

        /* This is another way to register the HttpClient and the HttpMessageSource, 
         * but it's not recommended because it can lead to issues with the lifetimes of the services, 
         * as the HttpMessageSource will be registered as transient and will be created every time it's resolved, 
         * which can lead to issues with the HttpClient, as it will be created every time the HttpMessageSource is created,
         * which can lead to issues with the performance and scalability of the application.
        services
            .AddHttpClient<IHttpMessageSource, HttpMessageSource>()
            .ConfigureHttpClient((serviceProvider, options) =>
            {
                //var httpMessageSource = serviceProvider.GetRequiredService<IOptions<...>>(); EXERCISE

                options.BaseAddress = new Uri("");
            });

        */


    });

using var host = builder.Build();
await host.RunAsync();




