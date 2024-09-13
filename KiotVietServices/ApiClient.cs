using BusinessObject.DTOs;
using KiotVietServices.Config;
using KiotVietServices.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly AccessTokenManager _accessTokenManager;
    private readonly string _retailer;
    private readonly string _apiBaseUrl;

    public ApiClient(IOptions<KiotVietConfig> kiotVietConfig, AccessTokenManager accessTokenManager)
    {

        _retailer = kiotVietConfig.Value.Retailer;

        _accessTokenManager = accessTokenManager;
        _apiBaseUrl = kiotVietConfig.Value.ApiBaseUrl;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_apiBaseUrl)
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Add("Retailer", _retailer);
    }

    private async Task CheckAndRenewAccessToken()
    {
        string accessToken = await _accessTokenManager.GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private StringContent CreateStringContent(object data)
    {
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        return new StringContent(jsonData, Encoding.UTF8, "application/json");
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        await CheckAndRenewAccessToken();

        using (var response = await _httpClient.GetAsync(endpoint))
        {
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<T>(response);
        }
    }

    public async Task<K> PostAsync<T, K>(string endpoint, T data)
    {
        await CheckAndRenewAccessToken();

        using (var response = await _httpClient.PostAsync(endpoint, CreateStringContent(data)))
        {
            if (response.IsSuccessStatusCode)
            {
                return await DeserializeResponse<K>(response);
            }
            else
            {
                var errorResponse = await DeserializeResponse<ErrorResponse>(response);

                throw new Exception(errorResponse.ResponseStatus.Message);
            }
        }
    }

    public async Task<K> PutAsync<T, K>(string endpoint, T data)
    {
        await CheckAndRenewAccessToken();

        using (var response = await _httpClient.PutAsync(endpoint, CreateStringContent(data)))
        {
            if (response.IsSuccessStatusCode)
            {
                return await DeserializeResponse<K>(response);
            }
            else
            {
                var errorResponse = await DeserializeResponse<ErrorResponse>(response);

                throw new Exception(errorResponse.ResponseStatus.Message);
            }
        }
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        await CheckAndRenewAccessToken();

        using (var response = await _httpClient.DeleteAsync(endpoint))
        {
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<T>(response);
        }
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        string responseData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<T>(responseData, options);
    }
}
