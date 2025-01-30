namespace APIUserValidation.Modules
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            //SAMPLE
            //services.AddSingleton<DapperContext>();
            //services.AddScoped<ICustomersApplication, CustomersApplication>();

            return services;
        }
    }
}
