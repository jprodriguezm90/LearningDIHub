using LearningDIHub.Circular;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
