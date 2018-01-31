using Isf.XCutting.Commands;
using Isf.XCutting.Logging;
using Isf.XCutting.Transactions;
using Logistics.Core.Stores;
using Logistics.Impl.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Web
{
    public static class DependencyExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddDbContext<LogisticsContext>(opt => opt.UseInMemoryDatabase("Logistics"));

            services.AddScoped<IInventoryStore, EfInventoryStore>();

            services.AddScoped<IUsernameProvider, StaticUsernameProvider>(provider => new StaticUsernameProvider("Test User"));

            services.AddScoped<ILogger, NLogLogger>();

            services.AddScoped<ITransaction, EfTransaction>();
            services.AddScoped<ITransactionFactory, EfTransactionFactory>();

            services.AddScoped<CommandContext, CommandContext>();

            //services.AddScoped<InventoryMasterManager, InventoryMasterManager>();
        }
    }
}
