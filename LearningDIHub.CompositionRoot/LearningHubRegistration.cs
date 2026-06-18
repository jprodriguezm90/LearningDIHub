using LearningDIHub.Domain.Auditing;
using LearningDIHub.Domain.DataSource;
using LearningDIHub.Domain.MessagesSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LearningDIHub.CompositionRoot
{
    public static class LearningHubRegistration
    {
        public static IServiceCollection AddLearningHub(this IServiceCollection services, IConfiguration configuration, Action<LearningHubConfiguration> lhConfigAction)
        {
            var lhConfig = new LearningHubConfiguration();
            lhConfigAction(lhConfig);
            if (lhConfig.RegisterAsHostedService)
            {
                services.AddHostedService<LearningHub>();
            }
            else
            {
                services.AddSingleton<LearningHub>();
            }

            services
            .AddMessageSelectorServices(configuration)
            .AddDataServices(configuration)
            .AddAuditingServices();
            return services;
        }
    }

    public sealed record LearningHubConfiguration
    {
        public bool RegisterAsHostedService { get; set; } = false;
    }
}
