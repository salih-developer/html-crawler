using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    using Nest;
    using System.ComponentModel.DataAnnotations;
    [Nest.ElasticsearchType(IdProperty = "Id", Name = "scrapingmodeldata")]
    public class ScrapingModelData
    {
        public ScrapingModelData()
        {
            this.CreateDate = DateTime.Now;
        }
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Id { get; set; }
        [Display(Name = "SiteUrl")]
        [String(Index =FieldIndexOption.NotAnalyzed)]
        public string SiteUrl { get; set; }
        [Display(Name = "Category"), Nest.String]
        public string Category { get; set; }
        [Display(Name = "Categorystr"), Nest.String]
        public string Categorystr { get; set; }
        [Display(Name = "Property"), Nest.String]
        public string Property { get; set; }

        [Display(Name = "Propertystr"), Nest.String]
        public string Propertystr { get; set; }
        [Display(Name = "Title"), Nest.String]
        public string Title { get; set; }
        [Display(Name = "Price"), Nest.String]
        public string Price { get; set; }
        [Display(Name = "DiscountPrice"), Nest.String]
        public string DiscountPrice { get; set; }
        [Display(Name = "Description"), Nest.String]
        public string Description { get; set; }
        [Display(Name = "Feature"), Nest.String]
        public string Feature { get; set; }
        [Display(Name = "Featurestr"), Nest.String]
        public string Featurestr { get; set; }
        [Display(Name = "Column1"), Nest.String]
        public string Column1 { get; set; }
        [Display(Name = "Column2"), Nest.String]
        public string Column2 { get; set; }
        [Display(Name = "Column3"), Nest.String]
        public string Column3 { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [Display(Name = "Owner"), Nest.String]
        public string Owner { get; set; }
        [Display(Name = "Phone"), Nest.String]
        public string Phone { get; set; }
        [Display(Name = "Adres"), Nest.String]
        public string Adres { get; set; }
        [Display(Name = "Firm"), Nest.String]
        public string Firm { get; set; }
        [Nest.Date]
        public DateTime CreateDate { get; set; }
        [Nest.Boolean(Index = NonStringIndexOption.NotAnalyzed)]
        public bool IsTransfer { get; set; }
    }
}
