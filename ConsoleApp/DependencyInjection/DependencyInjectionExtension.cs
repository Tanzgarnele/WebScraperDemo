using Microsoft.Extensions.DependencyInjection;
using WebScraperLibrary.Interfaces;
using WebScraperLibrary.Scrapers;
using WebScraperLibrary.Utilities;

namespace ConsoleApp.DependencyInjection
{
	public static class DependencyInjectionExtension
	{
		public static IServiceCollection RegisterAllDependencies(this IServiceCollection services)
		{
			if (services is null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			services.AddScoped<IScraper, AmazonScraper>();
			services.AddScoped<IWebScrapingUtility, WebScrapingUtility>();

			return services;
		}
	}
}