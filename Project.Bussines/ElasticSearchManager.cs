namespace Project.Bussines
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Nest;
    public class ElasticSearchManager
    {
        private ElasticClient CreateElasticClientInstance<T>(string defaultIndex) where T : class
        {

            var con = new ConnectionSettings(new Uri("http://51.15.224.11:9200/"));
            con.DefaultIndex(defaultIndex);
           
            var elk = new ElasticClient(con);
            if (!elk.IndexExists(Indices.Index<T>(), s => s.AllIndices()).Exists)
            {
                var ff = new CreateIndexDescriptor(defaultIndex)
                   .Mappings(ms => ms
                                  .Map<T>(m => m.AutoMap()));
                                 
                            
                var rst=     elk.CreateIndex(ff);
            }
            return elk;

        }

        public void Save<T>(T obj, string defaultIndex) where T:class 
        {
           var elk= CreateElasticClientInstance<T>(defaultIndex);
            elk.Index(obj, descriptor => descriptor.Index(defaultIndex));
        }

        public bool AnyBySiteUrl<T>(T obj, string defaultIndex,string url) where T : class
        {
            var elk = CreateElasticClientInstance<T>(defaultIndex);
           var rs=elk.Search<T>(t => t.Query(b => b.Term("siteUrl", url)));
            return rs.Documents.Any();
        }
    }
}
