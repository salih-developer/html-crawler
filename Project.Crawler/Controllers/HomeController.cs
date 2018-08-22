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
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using Microsoft.AspNetCore.Http;
    using Project.Common;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
         var list= typeof(CrawlerModel).GetProperties().Select(s => s.GetCustomAttributes(typeof(DisplayAttribute))).ToList().Select(c => ((DisplayAttribute)c.First()).Name).ToList();
            var model = new CrawlerViewModel
                        {
                             MapTags              = list
            };
            return this.View(new CrawlerViewModel{});
        }

        [HttpPost]
        public IActionResult About(string url)
        {
            var list = typeof(CrawlerModel).GetProperties().Select(s => s.GetCustomAttributes(typeof(DisplayAttribute))).ToList().Select(c => ((DisplayAttribute)c.First()).Name).ToList();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
            GetData(doc.DocumentNode.SelectSingleNode("//body"), ref keyValues);

            var model = new CrawlerViewModel
                        {
                            KeyValuePairs = keyValues,
                            HtmlDoc=doc.DocumentNode.InnerHtml,
                            MapTags = list
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
        [HttpPost]
        public IActionResult SaveXpath(IFormCollection form)
        {
            SortedList<string,string> keyValues=new SortedList<string, string>(); 
            foreach (var contain in form)
            {
                try
                {
                    if (contain.Key.Contains("cmb"))
                    {

                        keyValues.Add(contain.Value, form[contain.Key.Replace("cmb", "chck")]);
                    }
                }
                catch (Exception ex)
                {
                    
                }
             
            }

            var list = typeof(CrawlerModel).GetProperties().Select(s =>new{p=s,a= s.GetCustomAttributes(typeof(DisplayAttribute)).First() } ).ToList().Select(c =>new{p=c.p,n=((DisplayAttribute)c.a).Name}).ToList();
            CrawlerModel model=new  CrawlerModel();

            foreach (var item in list)
            {
                if (keyValues.ContainsKey(item.n))
                {
                    item.p.SetValue(model, keyValues[item.n]);
                }

            }
          
            return null;
        }

    }
}
