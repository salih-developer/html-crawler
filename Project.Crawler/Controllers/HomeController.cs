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
        public IActionResult About()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.morhipo.com/people-by-fabrika-pfkaw18el0010-volanli-abiye/21821153/detay?depid=20");

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


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
