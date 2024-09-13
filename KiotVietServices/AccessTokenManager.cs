using KiotVietServices.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AccessTokenManager
{
    private readonly string _authorityURL;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scope;
    private readonly string _grantType;

    private readonly KiotVietConfig _kiotVietConfig;

    private string _accessToken;
    private DateTime _accessTokenExpiration;

    public AccessTokenManager(IOptions<KiotVietConfig> kiotVietConfig)
    {
        this._kiotVietConfig = kiotVietConfig.Value;

        _authorityURL = "https://id.kiotviet.vn";
        _clientId = _kiotVietConfig.ClientId;
        _clientSecret = _kiotVietConfig.ClientSecret;
        _scope = _kiotVietConfig.Scopes;
        _grantType = _kiotVietConfig.GrantType;
    }

    public async Task<string> GetAccessToken()
    {
        if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _accessTokenExpiration)
        {
            await RequestNewAccessToken();
        }

        return _accessToken;
    }

    private async Task RequestNewAccessToken()
    {
        using (var httpClient = new HttpClient())
        {
            var content = new Dictionary<string, string>
            {
                { "scope", _scope },
                { "grant_type", _grantType },
                { "client_id", _clientId },
                { "client_secret", _clientSecret }
            };

            var formContent = new FormUrlEncodedContent(content);

            var response = await httpClient.PostAsync($"{_authorityURL}/connect/token", formContent);

            var contentType = formContent.Headers.ContentType;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                throw new Exception("Failed to obtain access token.");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var accessTokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(responseData, options);

            _accessToken = accessTokenResponse.access_token;
            _accessTokenExpiration = DateTime.UtcNow.AddSeconds(accessTokenResponse.expires_in);

            Console.WriteLine($"New Access Token: {_accessToken}");
            Console.WriteLine($"Token Expiration: {_accessTokenExpiration}");
        }
    }

    private class AccessTokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
