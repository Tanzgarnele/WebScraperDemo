using HtmlAgilityPack;

namespace WebScraperLibrary.Interfaces
{
	public interface IWebScrapingUtility
	{
		Task<String> GetHtmlWithRotatingUserAgentAsync(String url);

		Task<HtmlDocument> LoadHtmlDocumentAsync(String url);
	}
}