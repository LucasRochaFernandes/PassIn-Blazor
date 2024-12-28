using Microsoft.AspNetCore.Components.Authorization;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PassIn.Web.Services.Auth;

public class AuthAPIService : AuthenticationStateProvider
{
    protected readonly HttpClient _httpClient;

    public AuthAPIService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API-Server");
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var response = await _httpClient.GetAsync("auth/manage/info");

        var user = new ClaimsPrincipal();

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<ResponseAuthInfo>();
            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, content!.Email),
                new Claim(ClaimTypes.Email, content!.Email)
            ];

            var identity = new ClaimsIdentity(claims, "token_cookies");
            user = new ClaimsPrincipal(identity);
        }

        return new AuthenticationState(user);
    }

    public async Task<ResponseAuth> LogIn(RequestAuth request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login?useCookies=true", request);
        if (response.IsSuccessStatusCode)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return new ResponseAuth
            {
                Success = true,
                Errors = []
            };
        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync() ?? "Invalid Credentials";
            return new ResponseAuth
            {
                Success = false,
                Errors = [errorResponse]
            };
        }
    }

    public async Task LogOut()
    {
        await _httpClient.PostAsync("auth/logout", null);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
