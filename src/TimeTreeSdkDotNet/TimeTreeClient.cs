using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimeTreeSdkDotNet.Entities;
using TimeTreeSdkDotNet.Json;

namespace TimeTreeSdkDotNet;

/// <summary>
/// Provides a class for TimeTree API access.
/// </summary>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeTreeClient"/> class with the access token.
    /// </summary>
    /// <param name="accessToken">Access token.</param>
    public TimeTreeClient(string accessToken)
    {
        _accessToken = accessToken;
    }

    /// <summary>
    /// Retrieve information on the user authorized by access token.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Information on the user authorized by access token.</returns>
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