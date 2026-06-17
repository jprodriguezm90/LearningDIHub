using LearningDIHub.Domain.Auditing;
using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningDIHub.Domain.MessagesSender
{
    public static class MessageServicesRegistration
    {

        public static IServiceCollection AddMessageServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<MessageService>();
            services.AddTransient<TestController>();
            services.AddSingleton<IMessageService, MessageService>();

            //Testing multiple implementations of the same interface
            services.AddScoped<ISenderProvider, SMSProcessor>();
            services.AddScoped<ISenderProvider, EmailProcessor>();

            services.AddOptions<ServiceSelector>().Bind(config.GetSection(ServiceSelector.SectionName));

            services.AddTransient<A>();
            services.AddScoped<B>();

            return services;
        }
        public static IServiceCollection AddMessageSelectorServices(this IServiceCollection services, IConfiguration config)
        {
            //Note. you can't rely on ValidateScopes, because if you register a service as singleton,
            //it will be resolved as singleton even if it has a scoped dependency,
            //and this will not throw an exception until you try to resolve the service, which can lead to runtime errors.
            //So it's important to be careful when registering services and to avoid registering services with incompatible lifetimes.
            services.AddTransient<IMessageService, MessageService>();

            services.AddScoped<ISenderProvider, EmailProcessor>();

            services.AddOptions<ServiceSelector>().Bind(config.GetSection(ServiceSelector.SectionName));

            return services;
        }
    }
}
