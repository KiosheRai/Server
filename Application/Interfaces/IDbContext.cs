namespace Application.Interfaces
{
    public interface IDbContext
    {
        public INoteRepository Notes { get; }
    }
}
