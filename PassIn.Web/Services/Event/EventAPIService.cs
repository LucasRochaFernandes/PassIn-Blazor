
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using System.Net.Http.Json;

namespace PassIn.Web.Services.Event;

public class EventAPIService 
{
    protected readonly HttpClient _httpClient;

    public EventAPIService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API-Server");
    }

    public async Task<IList<ResponseEventJson>> GetAllEvents()
    {
        var response = await _httpClient.GetAsync("api/Events");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IList<ResponseEventJson>>() ?? [];
        }
        else {
            throw new Exception("API connection error");
        }
    }

    public async Task<ResponseRegisterJsonEventJson> RegisterEvent(RequestEventJson body)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Events", body);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ResponseRegisterJsonEventJson>();
        }
        else
        {
            throw new Exception("API connection error");
        }
    }

    public async Task DeleteEvent(Guid eventId)
    {
        var response = await _httpClient.DeleteAsync($"api/Events/{eventId}");
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        else
        {
            throw new Exception("API connection error");
        }

    }
}
