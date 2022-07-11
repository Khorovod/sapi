using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAPI.Data
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