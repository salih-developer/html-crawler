using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Crawler.Models
{
    public class CrawlerViewModel
    {
        public CrawlerViewModel()
        {
            KeyValuePairs=new List<KeyValuePair<string, string>>();
            MapTags=new List<string>();
        }
        public List<KeyValuePair<string, string>> KeyValuePairs { get; set; }
        public List<string> MapTags { get; set; }
        public string HtmlDoc { get; set; }
    }
}
