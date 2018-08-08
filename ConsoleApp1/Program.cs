using System;

namespace ConsoleApp1
{
    using HtmlAgilityPack;
    class Program
    {
        static void Main(string[] args)
        {
            HtmlAgilityPack.HtmlWeb htmlWeb=new HtmlWeb();
            var doc=htmlWeb.Load("https://www.morhipo.com/people-by-fabrika-pfkaw18el0010-volanli-abiye/21821153/detay?depid=20");
            var tt=doc.DocumentNode.SelectSingleNode("//*[@id='product-price']/div/div/div/span/span[2]/strong");
            var tt1 = doc.DocumentNode.SelectSingleNode("//*[@id='ela-urun-detay-marka']");
            var tt2 = doc.DocumentNode.SelectSingleNode("//*[@id='home-body-id']/div/div/ol");
            var tt3 = doc.DocumentNode.SelectSingleNode("//*[@id='aboutprodtab']/div/div");
            var tt4 = doc.DocumentNode.SelectSingleNode("//*[@id='carousel']/ul/li/a/img");
        }
    }
}
