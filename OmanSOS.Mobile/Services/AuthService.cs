﻿using Microsoft.Extensions.Configuration;
using OmanSOS.Core.ViewModels;
using System.Net.Http.Json;

namespace OmanSOS.Mobile.Services;

public interface IAuthService
{
    Task<ResponseViewModel<UserViewModel>?> Login(string email, string password);
}

public class AuthService : BaseService, IAuthService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public AuthService(IBrowserStorageService browserStorage, HttpClient http) : base(browserStorage)
    {
        _http = http;
        _baseUrl = $"{_http.BaseAddress}/auth";
    }

    public async Task<ResponseViewModel<UserViewModel>?> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/Login", new { email, password });
        return await response.Content.ReadFromJsonAsync<ResponseViewModel<UserViewModel>>();
    }
}
