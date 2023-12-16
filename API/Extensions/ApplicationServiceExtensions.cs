using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class ApplicationServiceExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(config.GetConnectionString("DeafultConnection"));
    });

        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();


        return services;
    }

}
