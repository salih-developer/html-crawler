using System;

namespace ConsoleApp1
{
    using System.IO;
    using System.Reflection;
    using HtmlAgilityPack;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            //using (var driver = new ChromeDriver("/home/dev", chromeOptions))
            {
                driver.Navigate().GoToUrl(@"https://www.morhipo.com/people-by-fabrika-pfkaw18el0010-volanli-abiye/21821153/detay?depid=20");
                var tt=driver.FindElement(By.XPath("//*[@id='product-price']/div/div/div/span/span[2]/strong"));
                Console.WriteLine(tt.Text.Trim());
                var tt1 = driver.FindElement(By.XPath("//*[@id='ela-urun-detay-marka']"));
                Console.WriteLine(tt1.Text.Trim());
                var tt2 = driver.FindElement(By.XPath("//*[@id='home-body-id']/div/div/ol"));
                Console.WriteLine(tt2.Text.Trim());
                var tt3 = driver.FindElement(By.XPath("//*[@id='aboutprodtab']/div/div"));
                Console.WriteLine(tt3.Text.Trim());
                //var tt4 = driver.FindElement(By.XPath("//*[@id='carousel']/ul/li/a/img"));
                //Console.WriteLine(tt4.Text.Trim());

                //var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
                //var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                //((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                //var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                //var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                //clickableElement.Click();
            }

            //HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlWeb();
            //var doc = htmlWeb.Load("https://www.morhipo.com/people-by-fabrika-pfkaw18el0010-volanli-abiye/21821153/detay?depid=20");
            //var tt = doc.DocumentNode.SelectSingleNode("//*[@id='product-price']/div/div/div/span/span[2]/strong");
            //Console.WriteLine(tt.InnerText.Trim());
            //var tt1 = doc.DocumentNode.SelectSingleNode("//*[@id='ela-urun-detay-marka']");
            //Console.WriteLine(tt1.InnerText.Trim());
            //var tt2 = doc.DocumentNode.SelectSingleNode("//*[@id='home-body-id']/div/div/ol");
            //Console.WriteLine(tt2.InnerText.Trim());
            //var tt3 = doc.DocumentNode.SelectSingleNode("//*[@id='aboutprodtab']/div/div");
            //Console.WriteLine(tt3.InnerText.Trim());
            //var tt4 = doc.DocumentNode.SelectSingleNode("//*[@id='carousel']/ul/li/a/img");
            //Console.WriteLine(tt4.InnerText.Trim());
            Console.Read();
        }
    }
}
