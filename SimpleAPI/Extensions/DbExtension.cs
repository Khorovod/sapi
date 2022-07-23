using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.Data;

namespace SimpleAPI.Extensions
{
    public static class DbExtension
    {
        public static void AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CommandContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("CommandSqlServer")
                ));
        }
    }
}