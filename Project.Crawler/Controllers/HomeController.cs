namespace Project.Crawler.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Project.Bussines;
    using Project.Common;
    using Project.Crawler.Models;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult About()
        {
            var list = typeof(ScrapingXpath).GetProperties().Select(s => s.GetCustomAttributes(typeof(DisplayAttribute))).ToList().Select(c => ((DisplayAttribute)c.First()).Name).ToList();
            var model = new CrawlerViewModel { MapTags = list };
            return this.View(new CrawlerViewModel());
        }
        [HttpPost]
        public IActionResult About(string url)
        {
            var list = typeof(ScrapingXpath).GetProperties().Select(s => s.GetCustomAttributes(typeof(DisplayAttribute))).ToList().Select(c => ((DisplayAttribute)c.First()).Name).ToList();
            var web = new HtmlWeb();

          var doc =  web.Load(url)
                        ;
            //foreach (var items in doc.DocumentNode.SelectNodes("//script"))
            //{
            //    items.Remove();
            //}
       
            //foreach (var htmlNode in doc.DocumentNode.ChildNodes.Where(x => x.XPath.Contains("#comment")).ToList())
            //{
            //    htmlNode.Remove();
            //}
            //foreach (var htmlNode in doc.DocumentNode.ChildNodes.Where(x => x.XPath.Contains("/#document")).ToList())
            //{
            //    htmlNode.Remove();
            //}
            //foreach (var selectNode in doc.DocumentNode.SelectNodes("//head/meta"))
            //{
            //    selectNode.Remove();
            //}
            var keyValues = new List<KeyValuePair<string, string>>();
            var model = new CrawlerViewModel { KeyValuePairs = keyValues, HtmlDoc = doc.DocumentNode.InnerHtml, MapTags = list, Url = url };
            return this.View(model);
        }
        private void GetData(HtmlNode documentNode, ref List<KeyValuePair<string, string>> keyValues)
        {
            foreach (var item in documentNode.ChildNodes)
            {
                keyValues.Add(new KeyValuePair<string, string>(item.XPath, item.InnerText));
                this.GetData(item, ref keyValues);
            }
        }
        public IActionResult PopupXpathSelect()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult SaveXpath(IFormCollection form)
        {
            var keyValues = new SortedList<string, string>();
            foreach (var contain in form)
                try
                {
                    if (contain.Key.Contains("cmb")) keyValues.Add(contain.Value, form[contain.Key.Replace("cmb", "chck")]);
                }
                catch (Exception ex)
                {
                }
            var list = typeof(ScrapingXpath).GetProperties().Select(s => new { p = s, a = s.GetCustomAttributes(typeof(DisplayAttribute)).First() }).ToList()
                                            .Select(c => new { c.p, n = ((DisplayAttribute)c.a).Name }).ToList();
            var model = new ScrapingXpath();
            foreach (var item in list)
                if (keyValues.ContainsKey(item.n))
                    item.p.SetValue(model, keyValues[item.n]);
            model.SiteUrl = form["SiteUrl"];

            model.Id = Guid.NewGuid().ToString();
            ElasticSearchManager elasticSearchManager=new ElasticSearchManager();
            elasticSearchManager.Save(model, "scrapingxpath");

            return null;
        }
    }
}
