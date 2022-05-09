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
        await using var connection = GetConnection();
        connection.Open();

        if (entity.RequestId != null)
            await connection.ExecuteAsync("UPDATE Requests SET IsClosed = 1 WHERE Id = @Id", new { Id = entity.RequestId });

        entity.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Donations (UserId, RequestId, LocationId, Amount, Remarks, CreatedBy, CreatedAt) Values (@UserId, @RequestId, @LocationId, @Amount, @Remarks, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
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
        const string? sql = "UPDATE Donations SET UserTypeId = @UserTypeId, RequestId = @RequestId, LocationId = @LocationId, Amount = @Amount, Remarks = @Remarks, IsActive = @IsActive, UpdatedBy = @UpdatedBy, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }

    public async Task<IEnumerable<Donation>> GetDonationsByUserId(int userId)
    {
        const string? sql = "SELECT * FROM Donations WHERE UserId = @UserId";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Donation>(sql, new { UserId = userId });
        return result == null ? new List<Donation>() : result.ToList();
    }

    public async Task<int> AddCampaign(Campaign campaign)
    {
        campaign.CreatedAt = DateTime.Now;
        const string? sql = "INSERT INTO Campaigns (Title, CampaignDate, CampaignTime, Remarks, CreatedBy, CreatedAt) Values (@Title, @CampaignDate, @CampaignTime, @Remarks, @CreatedBy, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        await using var connection = GetConnection();
        connection.Open();
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, campaign);
        return insertedId;
    }

    public async Task<IEnumerable<Campaign>> GetCampaigns()
    {
        const string? sql = "SELECT * FROM Campaigns ORDER BY Id DESC";
        await using var connection = GetConnection();
        connection.Open();
        var result = await connection.QueryAsync<Campaign>(sql);
        return result == null ? new List<Campaign>() : result.ToList();
    }
}
