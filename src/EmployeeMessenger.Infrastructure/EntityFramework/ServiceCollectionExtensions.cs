using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeMessenger.Infrastructure.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlDb(this IServiceCollection services, SqlSettings settings)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(settings.ConnectionString), ServiceLifetime.Transient);

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
