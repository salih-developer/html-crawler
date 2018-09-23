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
         ScrapingBusiness business=new ScrapingBusiness();
            business.Start();
        }
    }
}
