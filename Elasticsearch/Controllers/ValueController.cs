using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Model;
using Elasticsearch.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using NF.Common.Utility;

namespace Elasticsearch.Controllers
{

    public class ValueController : ControllerBase
    {
        private readonly ElasticClient _client;

        public ValueController(IEsClientProvider clientProvider)
        {
            _client = clientProvider.GetClient();
        }

        [HttpPost]
        [Route("value/index")]
        public IIndexResponse Index(Post post)
        {
            //Post post = new Post();
            return _client.IndexDocument(post);
        }

        [HttpPost]
        [Route("value/search")]
        public IReadOnlyCollection<Post> Seaarch(string type)
        {
            Singleton.getInstance().Init("ValueController").Error("demo .net core search error !!!!!");
            return _client.Search<Post>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Match(m => m.Field(f => f.Type).Query(type)))).Documents;
        }
    }
}