using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class LocationRepository : BaseRepository, ILocationRepository
{
    public LocationRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(Location entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Locations (Id, Name, CreatedBy, CreatedAt) Values (@Id, @Name, @CreatedBy, @CreatedAt); SELECT MAX(Id) FROM Locations";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM Locations";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM Locations WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM Locations";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Location>(sql);
        return result == null ? new List<Location>() : result.ToList();
    }

    public async Task<Location> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM Locations WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Location>(sql, new { Id = id });
        return result;
    }

    public async Task<int> GetNextId()
    {
        const string? sql = "SELECT MAX(Id) + 1 FROM Locations";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> UpdateAsync(int id, Location entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Locations SET Name = @Name, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
