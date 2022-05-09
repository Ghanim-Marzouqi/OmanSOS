using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Admin.Services;

public interface IDonationsService
{
    Task<ResponseViewModel<IEnumerable<DonationViewModel>>?> GetAll();

    Task<ResponseViewModel<DonationViewModel>?> GetById(int id);

    Task<ResponseViewModel<bool>?> Delete(int donationId);

    Task<ResponseViewModel<bool>?> AddCampaign(CampaignViewModel campaignViewModel);
}

public class DonationsService : BaseService, IDonationsService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public DonationsService(IBrowserStorageService browserStorage, IConfiguration configuration, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{configuration["ApiUrl"]}/donations";
    }

    public async Task<ResponseViewModel<IEnumerable<DonationViewModel>>?> GetAll()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<DonationViewModel>>?>($"{_baseUrl}/GetAll");
    }

    public async Task<ResponseViewModel<DonationViewModel>?> GetById(int id)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<DonationViewModel>?>($"{_baseUrl}/GetById/{id}");
    }

    public async Task<ResponseViewModel<bool>?> Delete(int donationId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/Delete/{donationId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
    }

    public async Task<ResponseViewModel<bool>?> AddCampaign(CampaignViewModel campaignViewModel)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/AddCampaign", campaignViewModel);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>>();
    }
}
