using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimeTreeSdkDotNet.Entities;
using TimeTreeSdkDotNet.Json;

namespace TimeTreeSdkDotNet;

public class TimeTreeClient
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://timetreeapis.com"),
        DefaultRequestHeaders =
        {
            Accept = { new MediaTypeWithQualityHeaderValue("application/vnd.timetree.v1+json") },
        },
    };
    private static readonly JsonSerializerOptions _jsonSeriazlizerOption = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
    };

    private readonly string _accessToken;

    public TimeTreeClient(string accessToken)
    {
        _accessToken = accessToken;
    }

    public async Task<TimeTreeEntity<TimeTreeUserAttribute>> GetUserAsync(CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/user");
        SetAuthorization(request);

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<TimeTreeOkResponse<TimeTreeEntity<TimeTreeUserAttribute>>>(_jsonSeriazlizerOption, cancellationToken);
        return content!.Data;
    }

    private void SetAuthorization(HttpRequestMessage requestMessage) => requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

    private struct TimeTreeOkResponse<T>
    {
        public T Data { get; set; }
    }
}