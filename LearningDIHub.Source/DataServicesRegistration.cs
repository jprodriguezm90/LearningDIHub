using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LearningDIHub.DataSource
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddOptions<HttpSourceOptions>().Bind(config.GetSection(HttpSourceOptions.SectionName));

            //Named Client
            /*services.AddHttpClient("message", options =>
            {
                options.BaseAddress = new Uri(config.GetSection("MessageSource:URI").Value);
            });
            services.AddKeyedTransient<IMessageSource, HttpMessageSource>("http");
            */

            //Typed Client
            services.AddHttpClient<HttpMessageSource>((serviceProvider,options) =>
            {
                var httpMessageSource = serviceProvider.GetRequiredService<IOptions<HttpSourceOptions>>();

                options.BaseAddress = new Uri(httpMessageSource.Value.URI);

                //This is a direct way to get configurations values 
                //options.BaseAddress = new Uri(config.GetSection("MessageSource:URI").Value);
            });

            services.AddKeyedTransient<IMessageSource, HttpMessageSource>("http", (sp, key) =>
            {
                return sp.GetRequiredService<HttpMessageSource>();
            });

            //End Typed Client

            services.AddKeyedTransient<IMessageSource, DBMessageSource>("db");

            // This is another way to register the HttpClient and the HttpMessageSource,
            /*services
                .AddHttpClient<IMessageSource, HttpMessageSource>()
                .ConfigureHttpClient((serviceProvider, options) =>
                {
                    var httpMessageSource = serviceProvider.GetRequiredService<IOptions<HttpSourceOptions>>();

                    options.BaseAddress = new Uri(httpMessageSource.Value.URI);
                });
            */
            
            return services;
        }
    }
}
