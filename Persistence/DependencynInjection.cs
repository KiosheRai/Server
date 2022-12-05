using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence
{
    public static class DependencynInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IDbContext, DbContext>();
        }
    }
}
