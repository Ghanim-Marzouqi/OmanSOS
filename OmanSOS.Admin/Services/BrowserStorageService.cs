using Microsoft.JSInterop;
using OmanSOS.Admin.Utilities;
using System.Text.Json;

namespace OmanSOS.Admin.Services
{
    public interface IBrowserStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }

    public class BrowserStorageService : IBrowserStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _browserStorage;

        public BrowserStorageService(IJSRuntime jsRuntime, string browserStorage = BrowserStorage.SESSION_STORAGE)
        {
            _jsRuntime = jsRuntime;
            _browserStorage = browserStorage;
        }

        public async Task<T> GetItem<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>($"{_browserStorage}.getItem", key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync($"{_browserStorage}.setItem", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync($"{_browserStorage}.removeItem", key);
        }
    }
}
