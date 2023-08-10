using HtmlAgilityPack;
using WebScraperLibrary.Interfaces;

namespace WebScraperLibrary.Utilities
{
	public class WebScrapingUtility : IWebScrapingUtility
	{
		private Int32 currentUserAgentIndex = 0;

		private readonly List<String> userAgents = new()
				{
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Safari/537.36",
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246",
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246",
					"Mozilla/5.0 (X11; CrOS x86_64 8172.45.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.64 Safari/537.36",
					"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36",
					"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.53 Safari/537.36"
				};

		public async Task<String> GetHtmlWithRotatingUserAgentAsync(String url)
		{
			if (String.IsNullOrWhiteSpace(url))
			{
				throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
			}

			String userAgent = this.userAgents[currentUserAgentIndex];
			currentUserAgentIndex = (currentUserAgentIndex + 1) % this.userAgents.Count;

			using HttpClient httpClient = new();
			httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

			HttpResponseMessage response = await httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		public async Task<HtmlDocument> LoadHtmlDocumentAsync(String url)
		{
			if (String.IsNullOrWhiteSpace(url))
			{
				throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
			}

			String content = await this.GetHtmlWithRotatingUserAgentAsync(url);
			HtmlDocument document = new();
			document.LoadHtml(content);
			return document;
		}
	}
}