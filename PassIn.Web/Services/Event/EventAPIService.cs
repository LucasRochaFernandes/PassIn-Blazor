
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using System.Net.Http.Json;

namespace PassIn.Web.Services.Event;

public class EventAPIService : APIService
{
    public EventAPIService(IHttpClientFactory factory) : base(factory)
    {
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
}
