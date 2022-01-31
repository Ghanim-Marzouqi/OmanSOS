using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IRequestsService
{
    Task<ResponseViewModel<bool>> Add(RequestViewModel request);

    Task<ResponseViewModel<IEnumerable<RequestViewModel>>> GetAll();

    Task<ResponseViewModel<RequestViewModel>> GetById(int id);

    Task<ResponseViewModel<bool>> Delete(int requestId);
}
public class RequestsService : BaseService, IRequestsService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public RequestsService(IBrowserStorageService browserStorage, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{_http.BaseAddress}/requests";
    }

    public async Task<ResponseViewModel<bool>> Add(RequestViewModel request)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/Add", request);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>>();
    }

    public async Task<ResponseViewModel<IEnumerable<RequestViewModel>>> GetAll()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<RequestViewModel>>>($"{_baseUrl}/GetAll");
    }

    public async Task<ResponseViewModel<RequestViewModel>> GetById(int id)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<RequestViewModel>>($"{_baseUrl}/GetById/{id}");
    }

    public async Task<ResponseViewModel<bool>> Delete(int requestId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/Delete/{requestId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>>();
    }
}
