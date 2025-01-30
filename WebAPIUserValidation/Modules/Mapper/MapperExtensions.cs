using ApiUserValidation.Services.Services;
using AutoMapper;

namespace APIUserValidation.Modules.Mapper
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            // Configuración de AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile()); // Agrega tu perfil aquí
            });

            IMapper mapper = mappingConfig.CreateMapper(); // Crea el mapeador
            services.AddSingleton(mapper); // Registra el mapeador como un singleton

            return services;
        }
    }
}
