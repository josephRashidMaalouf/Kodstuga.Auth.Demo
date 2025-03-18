using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazor.Dtos;
using Blazor.Events;
using Blazor.Interfaces;
using Blazored.LocalStorage;

namespace Blazor.Services;

public class AuthenticationStateProvider : IAuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public AuthenticationStateProvider(IHttpClientFactory factory, ILocalStorageService localStorageService)
    {
        _httpClient = factory.CreateClient("backend");
        _localStorageService = localStorageService;


    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _localStorageService.GetItemAsync<string>("token") ?? "";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await _httpClient.GetAsync("roles");

        return result.IsSuccessStatusCode;
    }

    public async Task<string> GetRoleAsync()
    {
        var token = await _localStorageService.GetItemAsync<string>("token") ?? "";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await _httpClient.GetAsync("roles");

        if (result.IsSuccessStatusCode)
        {
            var roles = await result.Content.ReadFromJsonAsync<List<GetRolesResponseDto>>();

            if (roles.Any())
            {
                return roles[0].value;
            }

        }

        return "";
    }
}