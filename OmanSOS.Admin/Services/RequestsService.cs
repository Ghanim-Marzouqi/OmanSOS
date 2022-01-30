using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Admin.Services;

public interface IRequestsService
{
    Task<ResponseViewModel<IEnumerable<RequestViewModel>>?> GetAll();
}
public class RequestsService : BaseService, IRequestsService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public RequestsService(IBrowserStorageService browserStorage, IConfiguration configuration, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{configuration["ApiUrl"]}/requests";
    }

    public async Task<ResponseViewModel<IEnumerable<RequestViewModel>>?> GetAll()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<RequestViewModel>>?>($"{_baseUrl}/GetAll");
    }
}
