using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Admin.Services;

public interface IMetadataService
{
    Task<ResponseViewModel<IEnumerable<CategoryViewModel>>?> GetCategories();

    Task<ResponseViewModel<IEnumerable<LocationViewModel>>?> GetLocations();

    Task<ResponseViewModel<IEnumerable<PriorityViewModel>>?> GetPriorities();

    Task<ResponseViewModel<int>?> GetNextCategoryId();

    Task<ResponseViewModel<int>?> GetNextLocationId();

    Task<ResponseViewModel<int>?> GetNextPriorityId();

    Task<ResponseViewModel<int>?> AddCategory(CategoryViewModel category);

    Task<ResponseViewModel<int>?> AddLocation(LocationViewModel location);

    Task<ResponseViewModel<int>?> AddPriority(PriorityViewModel priority);

    Task<ResponseViewModel<bool>?> DeleteCategory(int categoryId);

    Task<ResponseViewModel<bool>?> DeleteLocation(int locationId);

    Task<ResponseViewModel<bool>?> DeletePriority(int priorityId);
}

public class MetadataService : BaseService, IMetadataService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public MetadataService(IBrowserStorageService browserStorage, HttpClient http, IConfiguration configuration) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{configuration["ApiUrl"]}/metadata";
    }

    public async Task<ResponseViewModel<IEnumerable<CategoryViewModel>>?> GetCategories()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<CategoryViewModel>>>($"{_baseUrl}/GetCategories");
    }

    public async Task<ResponseViewModel<IEnumerable<LocationViewModel>>?> GetLocations()
    {
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<LocationViewModel>>>($"{_baseUrl}/GetLocations");
    }

    public async Task<ResponseViewModel<IEnumerable<PriorityViewModel>>?> GetPriorities()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<PriorityViewModel>>>($"{_baseUrl}/GetPriorities");
    }

    public async Task<ResponseViewModel<int>?> GetNextCategoryId()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<int>>($"{_baseUrl}/GetNextCategoryId");
    }

    public async Task<ResponseViewModel<int>?> GetNextLocationId()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<int>>($"{_baseUrl}/GetNextLocationId");
    }

    public async Task<ResponseViewModel<int>?> GetNextPriorityId()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<int>>($"{_baseUrl}/GetNextPriorityId");
    }

    public async Task<ResponseViewModel<int>?> AddCategory(CategoryViewModel category)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/AddCategory", category);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<int>?>();
    }

    public async Task<ResponseViewModel<int>?> AddLocation(LocationViewModel location)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/AddLocation", location);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<int>?>();
    }

    public async Task<ResponseViewModel<int>?> AddPriority(PriorityViewModel priority)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/AddPriority", priority);
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<int>?>();
    }

    public async Task<ResponseViewModel<bool>?> DeleteCategory(int categoryId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/DeleteCategory/{categoryId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
    }

    public async Task<ResponseViewModel<bool>?> DeleteLocation(int locationId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/DeleteLocation/{locationId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
    }

    public async Task<ResponseViewModel<bool>?> DeletePriority(int priorityId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/DeletePriority/{priorityId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
    }
}
