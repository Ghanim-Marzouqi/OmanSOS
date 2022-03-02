using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(Category entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Categories (Id, Name, CreatedBy, CreatedAt) Values (@Id, @Name, @CreatedBy, @CreatedAt); SELECT MAX(Id) FROM Categories";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM Categories";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM Categories WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM Categories";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Category>(sql);
        return result == null ? new List<Category>() : result.ToList();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM Categories WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
        return result;
    }

    public async Task<int> GetNextId()
    {
        const string? sql = "SELECT MAX(Id) + 1 FROM Categories";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> UpdateAsync(int id, Category entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Categories SET Name = @Name, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
