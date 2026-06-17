using LearningDIHub.Domain.Circular;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class CircularFuncRegistration
    {
        public static IServiceCollection AddFuncCircularRegistration(this IServiceCollection services)
        {
            //Testing Circular Dependencies with Func<T>
            services.AddSingleton<FirstClassFunc>();
            services.AddSingleton<Func<FirstClassFunc>>(csp => new Func<FirstClassFunc>(
                () => csp.GetRequiredService<FirstClassFunc>()));


            // WARNING: () => new FirstClassFunc(csp.GetRequiredService<ThirdClassFunc>()))); 
            // This is a case that can lead to runtime errors if not careful,
            // we are creating by our own the new instance of FirstClassFunc,
            // but as the program runs, a new instance will be created every time that Func<FirsClassFunc> its called.

            services.AddSingleton<SecondClassFunc>();
            services.AddTransient<ThirdClassFunc>();


            return services;
        }
    }
}
