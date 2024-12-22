
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
        var response = await _httpClient.GetAsync("Events");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IList<ResponseEventJson>>() ?? [];
        }
        else {
            throw new Exception("API connection error");
        }
    }
}
