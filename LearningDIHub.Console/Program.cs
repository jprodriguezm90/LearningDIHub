using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<MessageService>();
serviceCollection.AddTransient<TestController>();
serviceCollection.AddSingleton<IMessageService,MessageService>();

serviceCollection.AddTransient<ISenderProvider, SMSProcessor>();
serviceCollection.AddTransient<ISenderProvider, EmailProcessor>();

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

