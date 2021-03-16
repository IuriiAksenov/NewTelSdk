using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NewTelSdk.Extensions
{
    public static class AddNewTelExtension
    {
        private const string SectionName = "NewTel";

        public static IServiceCollection AddNewTel(this IServiceCollection services, string sectionName = SectionName)
        {
            if (services is null)
            {
                throw new ArgumentNullException($"{nameof(AddNewTel)}: {nameof(services)} is null.");
            }

            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var callPasswordOptions = (configuration ?? throw new NullReferenceException("IConfiguration is null")).GetOptions<NewTelOptions>(sectionName);
            services.AddSingleton(callPasswordOptions);
            services.AddSingleton(new CallPasswordApiClient(callPasswordOptions.AccessKey, callPasswordOptions.SignatureKey));
            return services;
        }
    }
}