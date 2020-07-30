using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Db
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AccountingDbContext>(
                options =>
                    options.UseInMemoryDatabase("Accounting")
            );

            return services;
        }
    }
}
