using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using HtmlAgilityPack;
    using OpenQA.Selenium;
    using Project.Bussines;
    using Project.Common;
    public class ScrapingBusiness
    {
        List<string> DetailPageUrlList = new List<string>();
        public void Start()
        {

            List<string> listpageUrls = new List<string>();
            listpageUrls.AddRange(new[]{"https://www.zingat.com/satilik-daire",
                                            "https://www.zingat.com/satilik-isyeri",
                                         "https://www.zingat.com/satilik-arsa",
                                            "https://www.zingat.com/satilik-yazlik",
                                            "https://www.zingat.com/satilik-villa",
                                            "https://www.zingat.com/satilik-dukkan-magaza",
                                            "https://www.zingat.com/kiralik-daire",
                                            "https://www.zingat.com/kiralik-isyeri",
                                            "https://www.zingat.com/kiralik-arsa",
                                            "https://www.zingat.com/kiralik-yazlik",
                                            "https://www.zingat.com/kiralik-villa",
                                            "https://www.zingat.com/kiralik-dukkan-magaza",
                                            "https://www.zingat.com/gunluk-kiralik-daire",
                                            "https://www.zingat.com/gunluk-kiralik-rezidans",
                                            "https://www.zingat.com/gunluk-kiralik-villa"});

            foreach (var listpageUrl in listpageUrls)
            {
                for (int i = 1; i <= 10; i++)
                {

                    GetListPageUrl(listpageUrl + "?page=" + i.ToString());
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    break;
                    
                }
            }

            foreach (var item in DetailPageUrlList)
            {
                GetDetailPageUrl(item);
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }

        }
        private void GetDetailPageUrl(string pageUrl)
        {
            Console.WriteLine(pageUrl);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            ScrapingModelData data = new ScrapingModelData();
            //using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            using (var driver = new ChromeDriver("/bin", chromeOptions))
            {
                driver.Navigate().GoToUrl(pageUrl);
                var gg = driver.PageSource;
                Console.WriteLine(gg);
                var doc = new HtmlDocument();
                doc.LoadHtml(gg);
                data.SiteUrl = pageUrl;
                data.Title = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[4]/div/div/div/div/h1").InnerText.Trim();
                data.Price = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[4]/div/div/div/div[2]/strong[2]").InnerText.Trim();
                data.Adres = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[4]/div/div/div/div/div/h2").InnerText.Trim();
                data.Owner = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[5]/div/div[2]/div/div/div/a").InnerText.Trim();
                data.Firm = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[5]/div/div[2]/div/div/div/a[2]").InnerText.Trim();
                data.Phone = string.Join(';', doc.DocumentNode.SelectNodes("//*[@class='contact-number-area number-area']/a").Select(x => x.Attributes["href"].Value));
                data.Property = doc.DocumentNode.SelectSingleNode("//*[@id='details']/div/div[5]/div/div/div[2]/div/div[2]").InnerText.Trim();
                data.Description = doc.DocumentNode.SelectSingleNode("//*[@id='detailDescription']/div/p").InnerText.Trim();
                data.Feature = doc.DocumentNode.SelectSingleNode("//*[@id='otherFacilities']/div").InnerText.Trim();
                data.Category = doc.DocumentNode.SelectSingleNode("//*[@id='breadcrumbContainer']/div/div/ol").InnerText.Trim();
                data.Picture =string.Join(',', doc.DocumentNode.SelectNodes("//div[@class='gallery-container']/a[@class='gallery-item zoon-in-image']").Select(x=>x.Attributes["data-lg"].Value));
            }

            ElasticSearchManager elasticSearchManager=new ElasticSearchManager();
            elasticSearchManager.Save(data, "scrapingmodeldata");

        }
        private void GetListPageUrl(string listpageUrl)
        {
            Console.WriteLine(listpageUrl);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--ignore-certificate-errors");

            //using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            using (var driver = new ChromeDriver("/bin", chromeOptions))
            {
                driver.Navigate().GoToUrl(listpageUrl);
                var gg = driver.PageSource;
                Console.WriteLine(gg);
                var doc = new HtmlDocument();
                doc.LoadHtml(gg);
                var r = doc.DocumentNode.SelectNodes("//*[@id='auctions']/main/div[2]/div/div[3]/div/a");
                var data = r.Select(x => "https://www.zingat.com" + x.Attributes["href"].Value).ToList();
                DetailPageUrlList.AddRange(data);

            }


        }
    }
}
