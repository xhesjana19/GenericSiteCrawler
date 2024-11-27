using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the URL:");
        string startUrl = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(startUrl))
        {
            Console.WriteLine("URL cannot be null or empty.");
            return;
        }

        List<string> urlList = new List<string>();

        Console.WriteLine($"Starting crawl at: {startUrl}");
        await CreateCrawlAsync(startUrl, urlList);

        Console.WriteLine("Crawling completed.");
    }

    /// <summary>
    /// Create Crawl method
    /// </summary>
    /// <param name="url"></param>
    /// <param name="visitedUrls"></param>
    /// <returns></returns>
    static async Task CreateCrawlAsync(string url, List<string> urlList)
    {
        if (urlList.Contains(url))
            return;

        Console.WriteLine($"Crawling: {url}");
        urlList.Add(url);

        try
        {
            using HttpClient client = new HttpClient();
            string pageContent = await client.GetStringAsync(url);

            string output = Path.Combine(Environment.CurrentDirectory, "CrawledContentPages");
            Directory.CreateDirectory(output);
            string fileName = Path.Combine(output, url.Replace("http://", "").Replace("https://", "").Replace("/", "") + ".html");

            File.WriteAllText(fileName, pageContent);

            Console.WriteLine($"Saved: {fileName}");

            List<string> allUrls = FindUrls(url, pageContent);

            foreach (string urlItem in allUrls)
            {
                // recursiv function
                await CreateCrawlAsync(urlItem, urlList);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to crawl {url}: {ex.Message}");
        }
    }

    /// <summary>
    /// Using HtmlAgilityPack 
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <param name="htmlContent"></param>
    /// <returns></returns>
    static List<string> FindUrls(string baseUrl, string htmlContent)
    {
        var links = new List<string>();
        try
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string secondUrl = node.GetAttributeValue("href", string.Empty);

                    if (string.IsNullOrWhiteSpace(secondUrl) || secondUrl.StartsWith("#") || secondUrl.StartsWith("mailto:"))
                        continue;

                    Uri baseUri = new Uri(baseUrl);
                    Uri absoluteUri = new Uri(baseUri, secondUrl);

                    if (absoluteUri.Host == baseUri.Host)
                        links.Add(absoluteUri.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to extract links: {ex.Message}");
        }

        return links;
    }
}
