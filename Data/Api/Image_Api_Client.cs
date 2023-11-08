using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ManGo.Data.Api
{
    class Image_Api_Client
    {
        static string baseUrl = "https://desu.me";
        HashSet<string> loadedImageUrls = new HashSet<string>();
        HtmlDocument doc = new HtmlDocument();
        public Image_Api_Client() { }
        public async Task<List<ImageSourse>> Loaded_NewAndUpdateManga(int swichFite)
        {
            string html = await DownloadHtmlAsync(baseUrl);
            List<ImageSourse> imageUrls = ExtractImageUrls(html, baseUrl, swichFite);

            foreach (ImageSourse imageData in imageUrls)
            {
                if (!loadedImageUrls.Contains(imageData.ImageURL))
                {
                    loadedImageUrls.Add(imageData.ImageURL);
                }
            }
            return imageUrls;
        }
        async Task<string> DownloadHtmlAsync(string baseUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Ошибка при загрузке HTML-страницы.");
                }
            }
        }

        private List<ImageSourse> ExtractImageUrls(string html, string baseUrl, int swichFite)
        {
            doc.LoadHtml(html);
            HashSet<string> uniqueImageUrls = new HashSet<string>();
            List<ImageSourse> imagesData = new List<ImageSourse>();

            switch (swichFite)
            {
                case 1:
                    IEnumerable<HtmlNode> divNodes = doc.DocumentNode.SelectNodes("//div[@class='slider__item']");
                    if (divNodes != null)
                    {
                        foreach (var divNode in divNodes)
                        {
                            IEnumerable<HtmlNode> imgNodes = divNode.SelectNodes(".//img[@class='img']");

                            var titleNode = divNode.SelectSingleNode(".//a[@class='title two_lined']");
                            string hrefValue = titleNode?.GetAttributeValue("href", "");

                            if (imgNodes != null || titleNode!= null)
                            {

                                foreach (var imgNode in imgNodes)
                                {
                                    string imageUrl = imgNode.GetAttributeValue("src", "");


                                    if (!string.IsNullOrWhiteSpace(imageUrl) && uniqueImageUrls.Add(imageUrl))
                                    {
                                        Uri fullUri;
                                        if (Uri.TryCreate(new Uri(baseUrl), imageUrl, out fullUri))
                                        {
                                            string decodedTitle = WebUtility.HtmlDecode(titleNode.InnerText);
                                            imagesData.Add(new ImageSourse
                                            {
                                                ImageURL = fullUri.AbsoluteUri,
                                                Text = decodedTitle,
                                                Href = hrefValue
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    var divNodes1 = doc.DocumentNode.SelectNodes("//div[@class='cc cc-similar d-cutted_covers']");
                    if (divNodes1 != null)
                    {
                        foreach (var divNode in divNodes1)
                        {
                            var imgNodes = divNode.SelectNodes(".//img[@class='img']");
                            var titleNodes = divNode.SelectNodes(".//a[@class='title two_lined']");
                            if (imgNodes != null && titleNodes != null)
                            {
                                var imgNodeList = imgNodes.ToList();
                                var titleNodeList = titleNodes.ToList();
                                int j = 0;
                                for (int i = 0; i < imgNodeList.Count; i++)
                                {
                                    string imageUrl = imgNodeList[i].GetAttributeValue("src", "");
                                    string decodedTitle = WebUtility.HtmlDecode(titleNodeList[j].InnerText);
                                    string hrefValue = titleNodeList[j].GetAttributeValue("href", "");

                                    if (!string.IsNullOrWhiteSpace(imageUrl))
                                    {
                                        Uri fullUri;
                                        if (Uri.TryCreate(new Uri(baseUrl), imageUrl, out fullUri))
                                        {
                                            imagesData.Add(new ImageSourse
                                            {
                                                ImageURL = fullUri.AbsoluteUri,
                                                Text = decodedTitle,
                                                Href = hrefValue
                                            });
                                            j++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
            return imagesData;
        }

        public string ReturnUrlTitle(string uri)
        {
            string fullUrl = baseUrl + '/' + uri;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = client.GetStringAsync(fullUrl).Result;

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    var linkNode = doc.DocumentNode.SelectSingleNode("//a[contains(@class, 'b-link_button') and contains(@class, 'dark') and contains(@class, 'read-online') and contains(@class, 'anime-date') and contains(@class, 'Tooltip')]");

                    if (linkNode != null)
                    {
                        string hrefValue = linkNode.GetAttributeValue("href", "");
                        return hrefValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return "error";
        }


        public Task<List<string>> ReturnImageUrls(string uri)
        {
            string fullUrl = baseUrl + '/' + uri;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-extensions");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(fullUrl);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                IWebElement preloadElement = driver.FindElement(By.Id("preload"));

                var imageUrls = preloadElement.FindElements(By.TagName("img"))
                    .Select(img => img.GetAttribute("src"))
                    .ToList();

                driver.Quit();
                return Task.FromResult(imageUrls);
            }
        }
     
    }
}

