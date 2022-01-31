using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IDonationsService
{
    Task<ResponseViewModel<bool>> Add(DonationViewModel donation);
}

public class DonationsService : BaseService, IDonationsService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public DonationsService(IBrowserStorageService browserStorage, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{_http.BaseAddress}/donations";
    }

    public async Task<ResponseViewModel<bool>> Add(DonationViewModel donation)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/Add", donation);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>>();
    }
}
