using Microsoft.Extensions.Configuration;
using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IUsersService
{
    Task<ResponseViewModel<IEnumerable<UserViewModel>>?> GetAll();

    Task<ResponseViewModel<bool>?> Delete(int userId);
}

public class UsersService : BaseService, IUsersService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public UsersService(IBrowserStorageService browserStorage, IConfiguration configuration, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{configuration["ApiUrl"]}/users";
    }

    public async Task<ResponseViewModel<IEnumerable<UserViewModel>>?> GetAll()
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        return await _http.GetFromJsonAsync<ResponseViewModel<IEnumerable<UserViewModel>>?>($"{_baseUrl}/GetAll");
    }

    public async Task<ResponseViewModel<bool>?> Delete(int userId)
    {
        _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
        var response = await _http.DeleteAsync($"{_baseUrl}/Delete/{userId}");
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
    }
}
