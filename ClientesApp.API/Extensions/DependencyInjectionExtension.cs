using ClientesApp.API.Contexts;
using ClientesApp.API.Repositories;

namespace ClientesApp.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();

            services.AddScoped<ClienteRepository>();
            services.AddScoped<EnderecoRepository>();

            return services;
        }
    }
}
