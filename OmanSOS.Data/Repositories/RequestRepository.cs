using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class RequestRepository : BaseRepository, IRequestRepository
{
    public RequestRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(Request entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Requests (CategoryId, LocationId, PriorityId, UserId, Description, CreatedBy, CreatedAt) Values (@CategoryId, @LocationId, @PriorityId, @UserId, @Description, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
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

    public async Task<Category> GetCategoryByRequestIdAsync(int requestId)
    {
        const string? sql = "SELECT c.* FROM Categories c INNER JOIN Requests r  ON c.Id = r.CategoryId WHERE r.Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = requestId });
        return result;
    }

    public async Task<Location> GetLocationByRequestIdAsync(int requestId)
    {
        const string? sql = "SELECT l.* FROM Locations l INNER JOIN Requests r  ON l.Id = r.LocationId WHERE r.Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Location>(sql, new { Id = requestId });
        return result;
    }

    public async Task<Priority> GetPriorityByRequestIdAsync(int requestId)
    {
        const string? sql = "SELECT p.* FROM Priorities p INNER JOIN Requests r  ON p.Id = r.PriorityId WHERE r.Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Priority>(sql, new { Id = requestId });
        return result;
    }

    public async Task<User> GetRequestorByRequestIdAsync(int requestId)
    {
        const string? sql = "SELECT u.* FROM Users u INNER JOIN Requests r  ON u.Id = r.UserId WHERE r.Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = requestId });
        return result;
    }

    public async Task<IEnumerable<Request>> GetRequestsByUserIdAsync(int userId)
    {
        const string? sql = "SELECT * FROM Requests WHERE UserId = @UserId";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Request>(sql, new { UserId = userId });
        return result;
    }

    public async Task<bool> IsOpenRequestExisted(int userId)
    {
        const string? sql = "SELECT * FROM Requests WHERE UserId = @UserId AND IsClosed = 0";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Request>(sql, new { UserId = userId });
        return result != null ? true : false;
    }
}
