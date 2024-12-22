
namespace PassIn.Web.Services;

public class APIService
{
    protected readonly HttpClient _httpClient;

    public APIService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API-Server");
    }

}
