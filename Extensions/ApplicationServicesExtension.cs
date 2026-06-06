using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.Data;
using raizes_do_nordeste.Middlewares;
using raizes_do_nordeste.Services;

namespace raizes_do_nordeste.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A variável de ambiente 'ConnectionStrings__DefaultConnection' não foi definida.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IPagamentoService, PagamentoService>();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
