using WebScraperLibrary.Models;

namespace WebScraperLibrary.Interfaces
{
	public interface IScraper
	{
		Task<ScapedProduct> ScrapeProductAsync(String url);
	}
}