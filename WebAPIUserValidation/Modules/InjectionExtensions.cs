using ApiUserValidation.Services.Services;

namespace APIUserValidation.Modules
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddScoped<JwtService>();

            //SAMPLE
            //services.AddSingleton<DapperContext>();
            //services.AddScoped<ICustomersApplication, CustomersApplication>();

            return services;
        }
    }
}
