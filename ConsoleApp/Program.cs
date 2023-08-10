using ConsoleApp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebScraperLibrary.Interfaces;
using WebScraperLibrary.Models;

namespace ConsoleApp;

internal class Program
{
	private static void Main(String[] args)
	{
		IHost host = CreateHostBuilder(args).Build();

		using (IServiceScope scope = host.Services.CreateScope())
		{
			IScraper scraper = scope.ServiceProvider.GetRequiredService<IScraper>();

			String productUrl = "https://www.amazon.de/Multivitamin-Pr%C3%A4parat-Antioxidantien-Wirkstoffkomplex-Traubenkernextrakt-Alpha-Lipons%C3%A4ure/dp/B00HCFV1AO?psc=1";
			ScapedProduct scrapedProduct = scraper.ScrapeProductAsync(productUrl).GetAwaiter().GetResult();

			Console.WriteLine($"Name: {scrapedProduct.Name}");
			Console.WriteLine($"Price: {scrapedProduct.PriceText}");
			Console.WriteLine($"ImgUrl: {scrapedProduct.ImageUrl}");
		}
	}

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.RegisterAllDependencies();
			});
}