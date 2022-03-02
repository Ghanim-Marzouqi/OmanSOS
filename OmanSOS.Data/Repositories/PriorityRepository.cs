using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class PriorityRepository : BaseRepository, IPriorityRepository
{
    public PriorityRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(Priority entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Priorities (Id, Name, CreatedBy, CreatedAt) Values (@Id, @Name, @CreatedBy, @CreatedAt); SELECT MAX(Id) FROM Priorities";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM Priorities";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM Priorities WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<Priority>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM Priorities";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Priority>(sql);
        return result == null ? new List<Priority>() : result.ToList();
    }

    public async Task<Priority> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM Priorities WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Priority>(sql, new { Id = id });
        return result;
    }

    public async Task<int> GetNextId()
    {
        const string? sql = "SELECT MAX(Id) + 1 FROM Priorities";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> UpdateAsync(int id, Priority entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Priorities SET Name = @Name, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
