using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Console
{
    public class LearningHub(IHostApplicationLifetime applicationLifetime, IServiceProvider serviceProvider) : IHostedService
    {
        public void Run()
        {
            System.Console.WriteLine($"Starting Learning Process");


            var msg = new Message() { Id = Guid.NewGuid(), From = "Dani", To = "Juan", Body = "I Love You More" };

            using var serviceScope = serviceProvider.CreateScope();
            var messageService = serviceScope.ServiceProvider.GetRequiredService<IMessageService>();
            var messageResult = messageService.SendMessage(msg);

            System.Console.WriteLine(messageResult);

        
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Run();
                return Task.CompletedTask;
            }
            finally
            {
                applicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
