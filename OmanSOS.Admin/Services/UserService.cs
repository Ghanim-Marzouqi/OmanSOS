using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Admin.Services
{

    public interface IUserService
    {
        Task<ResponseViewModel<bool>?> Add(UserViewModel user);

        Task<ResponseViewModel<IEnumerable<UserViewModel>>?> GetAll();

        Task<ResponseViewModel<bool>?> Delete(int userId);

    }

    public class UserService : BaseService, IUserService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public UserService(IBrowserStorageService browserStorage, IConfiguration configuration, HttpClient http) : base(browserStorage)
        {
            _http = http;
            _baseUrl = $"{configuration["ApiUrl"]}/users";
        }

        public async Task<ResponseViewModel<bool>?> Add(UserViewModel user)
        {
            _http.DefaultRequestHeaders.Authorization = await GetAuthorizationHeader();
            var response = await _http.PostAsJsonAsync($"{_baseUrl}/Add", user);
            return await response.Content.ReadFromJsonAsync<ResponseViewModel<bool>?>();
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
}
