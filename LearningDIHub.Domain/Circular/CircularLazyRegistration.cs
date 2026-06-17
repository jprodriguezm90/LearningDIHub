using LearningDIHub.Domain.Circular;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CircularLazyRegistration
    {
        public static IServiceCollection AddLazyCircularRegistration(this IServiceCollection services)
        {
            //Testing Circular Dependencies with Lazy<T> and Factory Methods
            services.AddSingleton<FirstClass>();
            services.AddSingleton<Lazy<FirstClass>>(csp => new Lazy<FirstClass>(
                () => csp.GetRequiredService<FirstClass>()));
            services.AddSingleton<SecondClass>();
            services.AddTransient<ThirdClass>();

            return services;
        }
    }
}
