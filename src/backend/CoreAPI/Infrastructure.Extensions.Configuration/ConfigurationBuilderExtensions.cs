using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions.Configuration;

public static class ConfigurationBuilderExtensions
{
	public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder, 
		string region,
		string secretName)
	{
		var configurationSource = 
			new AmazonSecretsManagerConfigurationSource(region, secretName);

		configurationBuilder.Add(configurationSource);
	}
}
