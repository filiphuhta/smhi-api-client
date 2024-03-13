public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public HttpService(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    public async Task<HttpResponseMessage> GetRequest(string endpoint)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + endpoint);
            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            throw;
        }
    }
}