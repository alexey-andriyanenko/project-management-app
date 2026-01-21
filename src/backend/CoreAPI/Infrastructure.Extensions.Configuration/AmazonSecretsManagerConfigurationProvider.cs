using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions.Configuration;

public class AmazonSecretsManagerConfigurationProvider(string region, string secretName) : ConfigurationProvider
{
    public override void Load()
    {
        var secret = GetSecret();

        using var doc = JsonDocument.Parse(secret);
        var dict = new Dictionary<string, string?>();
        FlattenJsonElement("", doc.RootElement, dict);

        Data = dict;
    }

    private void FlattenJsonElement(string prefix, JsonElement element, IDictionary<string, string?> dict)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var prop in element.EnumerateObject())
                {
                    var key = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}:{prop.Name}";
                    FlattenJsonElement(key, prop.Value, dict);
                }
                break;
            case JsonValueKind.Array:
                int i = 0;
                foreach (var item in element.EnumerateArray())
                {
                    FlattenJsonElement($"{prefix}:{i}", item, dict);
                    i++;
                }
                break;
            default:
                dict[prefix] = element.ToString();
                break;
        }
    }

    private string GetSecret()
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT"
        };

        using var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
        var response = client.GetSecretValueAsync(request).Result;

        if (!string.IsNullOrEmpty(response.SecretString))
            return response.SecretString;

        using var reader = new StreamReader(response.SecretBinary);
        var base64 = reader.ReadToEnd();
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    }
}