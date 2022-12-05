using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Note>> GetAllAsync();
        Task<IReadOnlyList<Note>> GetRangeAsync(int start, int end);
        Task<int> AddAsync(Note entity);
        Task<int> UpdateAsync(Note entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> CompleteAsync(Guid id);
    }
}
