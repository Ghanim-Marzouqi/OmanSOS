using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IDonationsService
{
    Task<ResponseViewModel<bool>> Add(DonationViewModel donation);

    Task<ResponseViewModel<IEnumerable<DonationViewModel>>> GetAll();

    Task<ResponseViewModel<DonationViewModel>> GetById(int id);

    Task<ResponseViewModel<IEnumerable<DonationViewModel>>> GetDonationsByUserId(int userId);
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

    public async Task<ResponseViewModel<IEnumerable<DonationViewModel>>> GetAll()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<DonationViewModel>>>($"{_baseUrl}/GetAll");
    }

    public async Task<ResponseViewModel<DonationViewModel>> GetById(int id)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<DonationViewModel>>($"{_baseUrl}/GetById/{id}");
    }

    public async Task<ResponseViewModel<IEnumerable<DonationViewModel>>> GetDonationsByUserId(int userId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<DonationViewModel>>>($"{_baseUrl}/GetDonationsByUserId/{userId}");
    }
}
