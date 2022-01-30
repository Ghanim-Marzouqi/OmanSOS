using Dapper;
using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Interfaces;
using OmanSOS.Core.Models;

namespace OmanSOS.Data.Repositories;

public class DonationRepository : BaseRepository, IDonationRepository
{
    public DonationRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddAsync(Donation entity)
    {
        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Donations (UserId, RequestId, Amount, CreatedBy, CreatedAt) Values (@UserId, @RequestId, @Amount, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, entity);
        return insertedId;
    }

    public async Task<int> CountAsync()
    {
        const string? sql = "SELECT COUNT(*) FROM Donations";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }

    public async Task<int> DeleteAsync(int id)
    {
        const string? sql = "DELETE FROM Donations WHERE Id = @Id;";
        await using var connection = GetConnection();
        connection.Open();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows;
    }

    public async Task<IEnumerable<Donation>> GetAllAsync()
    {
        const string? sql = "SELECT * FROM Donations";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Donation>(sql);
        return result == null ? new List<Donation>() : result.ToList();
    }

    public async Task<Donation> GetByIdAsync(int id)
    {
        const string? sql = "SELECT * FROM Donations WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<Donation>(sql, new { Id = id });
        return result;
    }

    public async Task<int> UpdateAsync(int id, Donation entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.Now;
        const string? sql = "UPDATE Donations SET UserTypeId = @UserTypeId, RequestId = @RequestId, Amount = @Amount, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }
}
