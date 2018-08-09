namespace Project.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class CrawlerModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Display(Name = "DiscountPrice")]
        public string DiscountPrice { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Properties")]
        public List<KeyValuePair<string, string>> Properties { get; set; }
    }
}
