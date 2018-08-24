namespace Project.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Nest;
    [Serializable]
    [ElasticsearchType(IdProperty = "Id", Name = "scrapingXpath")]
    public class ScrapingXpath
    {
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "SiteUrl")]
        [String]
        public string SiteUrl { get; set; }
        [Display(Name = "Category")]
        [String]
        public string Category { get; set; }
        [Display(Name = "Property")]
        [String]
        public string Property { get; set; }
        [Display(Name = "Title")]
        [String]
        public string Title { get; set; }
        [Display(Name = "Price")]
        [String]
        public string Price { get; set; }
        [Display(Name = "DiscountPrice")]
        [String]
        public string DiscountPrice { get; set; }
        [Display(Name = "Description")]
        [String]
        public string Description { get; set; }
        [Display(Name = "Feature")]
        [String]
        public string Feature { get; set; }
        [Display(Name = "Column1")]
        [String]
        public string Column1 { get; set; }
        [Display(Name = "Column2")]
        [String]
        public string Column2 { get; set; }
        [Display(Name = "Column3")]
        [String]
        public string Column3 { get; set; }
        [Display(Name = "Column4")]
        [String]
        public string Column4 { get; set; }
        [Display(Name = "Picture")]
        [String]
        public string Picture { get; set; }
    }
}
