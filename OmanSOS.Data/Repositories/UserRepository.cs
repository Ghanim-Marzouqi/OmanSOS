using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(User entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Users (UserTypeId, NationalId, LocationId, Name, Email, Phone, PasswordHash, PasswordSalt, CreatedBy, CreatedAt) Values (@UserTypeId, @NationalId, @LocationId, @Name, @Email, @Phone, @PasswordHash, @PasswordSalt, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM Users";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM Users WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM Users";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<User>(sql);
        return result == null ? new List<User>() : result.ToList();
    }

    public async Task<User?> GetByEmailAsync(string? email)
    {
        if (email == null) return null;
        const string? sql = "SELECT * FROM Users WHERE Email = @Email";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        return result;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM Users WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        return result;
    }

    public async Task<User?> GetByNationalIdAsync(int nationalId)
    {
        const string? sql = "SELECT * FROM Users WHERE NationalId = @NationalId";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { NationalId = nationalId });
        return result;
    }

    public async Task<int> UpdateAsync(int id, User entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Users SET UserTypeId = @UserTypeId, Name = @Name, Email = @Email, Phone = @Phone, Location = @Location, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
