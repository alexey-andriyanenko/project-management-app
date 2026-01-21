using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions.Configuration;

public class AmazonSecretsManagerConfigurationSource(string region, string secretName) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AmazonSecretsManagerConfigurationProvider(region, secretName);
    }
}