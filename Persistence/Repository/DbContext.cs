using Application.Interfaces;

namespace Persistence.Repository
{
    public class DbContext : IDbContext
    {
        public INoteRepository Notes { get; }

        public DbContext(INoteRepository noteRepository)
        {
            Notes = noteRepository;
        }
    }
}
