using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Project.Crawler.Models;

namespace Project.Crawler.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            return this.View(new CrawlerViewModel{});
        }

        [HttpPost]
        public IActionResult About(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
            GetData(doc.DocumentNode.SelectSingleNode("//body"), ref keyValues);

            var model = new CrawlerViewModel
                        {
                            KeyValuePairs = keyValues,
                            HtmlDoc=doc.DocumentNode.InnerHtml
                        };
            return View(model);
        }

        private void GetData(HtmlNode documentNode, ref List<KeyValuePair<string, string>> keyValues)
        {
            foreach (var item in documentNode.ChildNodes)
            {
                keyValues.Add(new KeyValuePair<string, string>(item.XPath, item.InnerText));
                GetData(item, ref keyValues);
            }
        }
        public IActionResult PopupXpathSelect()
        {
            return View();
        }
        public IActionResult SaveXpath()
        {
            return null;
        }

    }
}
