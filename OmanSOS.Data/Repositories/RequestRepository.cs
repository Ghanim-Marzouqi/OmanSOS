using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        public RequestRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddAsync(Request entity)
        {
            entity.CreatedAt = DateTime.Now;
            const string? sql = "INSERT INTO Requests (CategoryId, PriorityId, UserId, Description, Location, CreatedBy, CreatedAt) Values (@CategoryId, @PriorityId, @UserId, @Description, @Location, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            await using var connection = GetConnection();
            connection.Open();
            var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
            return insertedId;
        }

        public async Task<int> CountAsync()
        {
            const string? sql = "SELECT COUNT(*) FROM Requests";
            await using var connection = GetConnection();
            connection.Open();
            var result = await connection.ExecuteScalarAsync<int>(sql);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            const string? sql = "DELETE FROM Requests WHERE Id = @Id;";
            await using var connection = GetConnection();
            connection.Open();
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows;
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            const string? sql = "SELECT * FROM Requests";
            await using var connection = GetConnection();
            connection.Open();
            var result = await connection.QueryAsync<Request>(sql);
            return result == null ? new List<Request>() : result.ToList();
        }

        public async Task<Request> GetByIdAsync(int id)
        {
            const string? sql = "SELECT * FROM Requests WHERE Id = @Id";
            await using var connection = GetConnection();
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Request>(sql, new { Id = id });
            return result;
        }

        public async Task<int> UpdateAsync(int id, Request entity)
        {
            entity.Id = id;
            entity.UpdatedAt = DateTime.Now;
            const string? sql = "UPDATE Requests SET CategoryId = @CategoryId, PriorityId = @PriorityId, UserId = @UserId, Description = @Description, Location = @Location, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            await using var connection = GetConnection();
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
}
