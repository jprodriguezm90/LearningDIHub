using LearningDIHub.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LearningDIHub.Auditing
{
    public static class AuditingServiceRegistration
    {
        public static IServiceCollection AddAuditingServices(this IServiceCollection services)
        {

            //All Registration of type AuditLogger is done with one line
            services.AddSingleton(typeof(IAuditLogger<>), typeof(AuditLogger<>));

            services.AddSingleton<IAuditLogger<Message>, MessageAuditLogger>();

            return services;
        }
    }
}
