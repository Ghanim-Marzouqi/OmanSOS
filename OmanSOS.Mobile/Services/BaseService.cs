using OmanSOS.Mobile.Utilities;
using OmanSOS.Core.ViewModels;
using System.Net.Http.Headers;

namespace OmanSOS.Mobile.Services;

public class BaseService
{
    private readonly IBrowserStorageService _browserStorage;

    public BaseService(IBrowserStorageService browserStorage)
    {
        _browserStorage = browserStorage;
    }

    public async Task<AuthenticationHeaderValue> GetAuthorizationHeader()
    {
        var user = await _browserStorage.GetItem<UserViewModel>("user");
        return new AuthenticationHeaderValue(AuthorizationType.Bearer, user.AccessToken);
    }

    public string GetBaseUri()
    {
        return "https://10.0.2.2:5001/api";
    }
}
