using Application.Interfaces;
using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IConfiguration _configuration;

        public NoteRepository(IConfiguration configuration) =>
            _configuration = configuration;

        public async Task<int> AddAsync(Note entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Id = Guid.NewGuid();
            entity.IsCompleted = false;
            entity.IsDeleted = false;
            var sql = "INSERT INTO Notes (Id, [Name], [Description], CreatedDate, CompletedDate, IsCompleted, IsDeleted) VALUES (@Id, @Name, @Description, @CreatedDate, @CompletedDate, @IsCompleted, @IsDeleted);";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> CompleteAsync(Guid id)
        {
            var sql = "UPDATE Notes SET IsCompleted = 1, CompletedDate = @CompletedDate WHERE Id = @id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { CompletedDate = DateTime.Now, Id = id });
                return result;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = "UPDATE Notes SET IsDeleted = 1 WHERE id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Note>> GetAllAsync()
        {
            var sql = "SELECT * FROM Notes WHERE IsDeleted = 0";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Note>(sql);
                return result.ToList();
            }
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Notes WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Note>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Note>> GetRangeAsync(int start, int end)
        {
            var sql = "SELECT * FROM Notes WHERE IsDeleted = 0 ORDER BY [CreatedDate]";

            IEnumerable<Note> result;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var resultQuery = await connection.QueryAsync<Note>(sql);
                if (end >= start)
                {
                    result = resultQuery
                        .Skip(start - 1)
                        .Take(end - start + 1);
                }
                else
                {
                    result = resultQuery
                       .Skip(end - 1)
                       .Take(start - end + 1)
                       .Reverse();
                }
                return result.ToList();
            }
        }

        public async Task<int> UpdateAsync(Note entity)
        {
            var sql = "UPDATE Notes SET Name = @Name, Description = @Description";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
