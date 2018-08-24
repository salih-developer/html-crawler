namespace Project.Bussines
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Nest;
    public class ElasticSearchManager
    {
        private ElasticClient CreateElasticClientInstance<T>(string defaultIndex)
        {

            var con = new ConnectionSettings(new Uri("http://51.15.224.11:9200/"));
            con.DefaultIndex(defaultIndex);
           
            var elk = new ElasticClient(con);
            if (!elk.IndexExists(Indices.Index<T>(), s => s.AllIndices()).Exists)
            {
           var rst=     elk.CreateIndex(
                                IndexName.From<T>(),
                                descriptor => descriptor.Mappings(mappingsDescriptor => mappingsDescriptor.Map(TypeName.From<T>(), mappingDescriptor => mappingDescriptor.AutoMap(10))));
            }
            return elk;

        }

        public void Save<T>(T obj, string defaultIndex) where T:class 
        {
           var elk= CreateElasticClientInstance<T>(defaultIndex);
            elk.Index(obj, descriptor => descriptor.Index(defaultIndex));
        }
        
    }
}
