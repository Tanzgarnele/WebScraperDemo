using HtmlAgilityPack;
using WebScraperLibrary.Interfaces;
using WebScraperLibrary.Models;

namespace WebScraperLibrary.Scrapers
{
	public class AmazonScraper : IScraper
	{
		private readonly IWebScrapingUtility webScrapingUtility;

		public AmazonScraper(IWebScrapingUtility webScrapingUtility)
		{
			this.webScrapingUtility = webScrapingUtility ?? throw new ArgumentNullException(nameof(webScrapingUtility));
		}

		public async Task<ScapedProduct> ScrapeProductAsync(String url)
		{
			if (String.IsNullOrWhiteSpace(url))
			{
				throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
			}

			try
			{
				HtmlDocument doc = await webScrapingUtility.LoadHtmlDocumentAsync(url);

				HtmlNode priceNode = doc.DocumentNode.SelectSingleNode("//span[@class='a-price']//span[@class='a-offscreen']");
				HtmlNode imgNode = doc.DocumentNode.SelectSingleNode("//img[@id='landingImage']");
				HtmlNode productNameNode = doc.DocumentNode.SelectSingleNode("//span[@id='productTitle']");

				String priceText = priceNode?.InnerText?.Replace("€", "").Trim() ?? "N/A";
				String imageUrl = imgNode?.GetAttributeValue("src", "N/A") ?? "N/A";
				String productName = productNameNode?.InnerText?.Trim() ?? "N/A";

				return new ScapedProduct
				{
					PriceText = priceText,
					ImageUrl = imageUrl,
					Name = productName
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				//TODO: Add Logging
				return null;
			}
		}
	}
}