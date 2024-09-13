namespace KiotVietServices.Config;

public class KiotVietConfig
{
    public static string ConfigName => "KiotViet";
    public string Scopes { get; set; } = string.Empty;
    public string GrantType { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Retailer { get; set; } = string.Empty;
    public string ApiBaseUrl { get; set; } = string.Empty;
}
