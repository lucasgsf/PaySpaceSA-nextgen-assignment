using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace PaySpace.Calculator.Web.Data.Helpers;

public static class ApiHelper
{
    public static async Task<T?> Get<T>(string requestUri, HttpClient httpClient,CancellationToken cancellationToken = default) where T : class =>
        (await Send<T>(HttpMethod.Get, requestUri, httpClient, null, cancellationToken));

    public static async Task<T> Post<T>(string requestUri, HttpClient httpClient, object? data, CancellationToken cancellationToken = default) where T : class =>
        (await Send<T>(HttpMethod.Post, requestUri, httpClient, data, cancellationToken))!;

    private static async Task<T?> Send<T>(HttpMethod httpMethod, string requestUri, HttpClient httpClient, object? data, CancellationToken cancellationToken) where T : class
    {
        using var requestMessage = new HttpRequestMessage(httpMethod, requestUri);

        if (httpMethod != HttpMethod.Get && data is not null)
            requestMessage.Content = JsonContent.Create(data);

        using var response = await httpClient.SendAsync(requestMessage, cancellationToken);

        try
        {
            if (response.IsSuccessStatusCode)
                return await DeserializeHttpResponseMessage<T>(response);

            if (httpMethod == HttpMethod.Get && response.StatusCode == HttpStatusCode.NotFound)
                return null;

            var code = response.StatusCode switch
            {
                HttpStatusCode.BadRequest or HttpStatusCode.NotFound => response.StatusCode,
                _ => HttpStatusCode.BadGateway
            };

            throw await HandleException(requestUri, response, code, null);
        }
        catch (JsonException ex)
        {
            throw await HandleException(requestUri, response, response.StatusCode, ex);
        }
    }

    private static async Task<T> DeserializeHttpResponseMessage<T>(HttpResponseMessage httpResponse)
    {
        try
        {
            var json = await httpResponse.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(json, typeof(T));

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private static async Task<Exception> HandleException(string requestUri, HttpResponseMessage response, HttpStatusCode statusCode, JsonException? exception)
    {
        var rawBody = await response.Content.ReadAsStringAsync();
        var messageError = $@"API replied with an invalid body. Request path: {requestUri} Status code: {response.StatusCode} Body: {rawBody}";
        return new Exception(messageError, null);
    }
}