using LearningDIHub.Domain.Auditing;
using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.MessagesSender;
using LearningDIHub.Domain.Models;
using LearningDIHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.DataSource
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
