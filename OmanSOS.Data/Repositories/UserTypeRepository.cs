using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class UserTypeRepository : BaseRepository, IUserTypeRepository
{
    public UserTypeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(UserType entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO UserTypes (Id, Name, CreatedBy, CreatedAt) Values (@Id, @Name, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM UserTypes";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM UserTypes WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<UserType>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM UserTypes";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<UserType>(sql);
        return result == null ? new List<UserType>() : result.ToList();
    }

    public async Task<UserType> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM UserTypes WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<UserType>(sql, new { Id = id });
        return result;
    }

    public async Task<int> UpdateAsync(int id, UserType entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Users SET Name = @Name, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
