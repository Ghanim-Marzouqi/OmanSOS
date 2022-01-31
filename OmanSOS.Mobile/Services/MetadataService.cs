using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IMetadataService
{
    Task<ResponseViewModel<IEnumerable<CategoryViewModel>>> GetCategories();

    Task<ResponseViewModel<IEnumerable<PriorityViewModel>>> GetPriorities();
}

public class MetadataService : BaseService, IMetadataService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public MetadataService(IBrowserStorageService browserStorage, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{_http.BaseAddress}/metadata";
    }

    public async Task<ResponseViewModel<IEnumerable<CategoryViewModel>>> GetCategories()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<CategoryViewModel>>>($"{_baseUrl}/GetCategories");
    }

    public async Task<ResponseViewModel<IEnumerable<PriorityViewModel>>> GetPriorities()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<PriorityViewModel>>>($"{_baseUrl}/GetPriorities");
    }
}
