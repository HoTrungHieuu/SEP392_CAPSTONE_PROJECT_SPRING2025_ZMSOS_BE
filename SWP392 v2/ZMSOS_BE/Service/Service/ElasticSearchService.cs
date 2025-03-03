using DAO.ViewModel;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;
using BO.Models;


namespace Service.Service
{
    public class ElasticSearchService
    {
        public ElasticSearchService()
        {
            
        }
        public async Task<List<AnimalView>> ListAnimalViewSearch(List<AnimalView> animals, string search)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node)
                .DefaultIndex("animals");
            var client = new ElasticClient(settings);
            var createIndexResponse = await client.Indices.CreateAsync("animals", c => c
            .Map<AnimalView>(m => m.AutoMap())
            );
            foreach ( var animal in animals )
            {
                var indexResponse = await client.IndexAsync(animal, idx => idx.Index("animals"));
            }
            var searchResponse = await client.SearchAsync<AnimalView>(s => s
            .Index("animals")
            .Query(q => q
             .Match(m => m
            .Field(f => f.AnimalType.EnglishName)
            .Query(search)
            )
            )
            );
            return searchResponse.Documents.ToList();
        }
    }
}
