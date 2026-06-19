using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace LearningDIHub.MessageSender
{
    public static class MessageServicesRegistration
    {

        public static IServiceCollection AddMessageServices(this IServiceCollection services, IConfiguration config)
        {
            //Note. you can't rely on ValidateScopes, because if you register a service as singleton,
            //it will be resolved as singleton even if it has a scoped dependency,
            //and this will not throw an exception until you try to resolve the service, which can lead to runtime errors.
            //So it's important to be careful when registering services and to avoid registering services with incompatible lifetimes.

            services.Scan(scanner => scanner.FromAssemblyOf<ISenderProvider>()
                .AddClasses(c => c.WithAttribute<AsSingletonAttribute>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            services.Scan(scanner => scanner.FromAssemblyOf<ISenderProvider>()
                .AddClasses(c => c.WithAttribute<AsTransientAttribute>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(scanner => scanner.FromAssemblyOf<ISenderProvider>()
                .AddClasses(c => c.WithoutAttribute<DoNotRegisterAttribute>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip) // Skip already registered services to avoid conflicts with singleton services
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddOptions<ServiceSelector>().Bind(config.GetSection(ServiceSelector.SectionName));

            services.Decorate<IMessageService, MessageServiceLoggingDecorator>();

            return services;
        }
        public static IServiceCollection AddSimpleMessageServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<MessageService>();
            services.AddTransient<TestController>();
            services.AddTransient<IMessageService, MessageService>();

            //Testing multiple implementations of the same interface
            services.AddScoped<ISenderProvider, SMSProcessor>();
            services.AddScoped<ISenderProvider, EmailProcessor>();

            services.AddTransient<A>();
            services.AddScoped<B>();
            

            services.AddOptions<ServiceSelector>().Bind(config.GetSection(ServiceSelector.SectionName));


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
    public sealed class AsSingletonAttribute : Attribute {}
    public sealed class DoNotRegisterAttribute : Attribute {}
    public sealed class AsTransientAttribute : Attribute { }
}
