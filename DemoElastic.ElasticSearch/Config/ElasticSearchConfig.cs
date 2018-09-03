using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoElastic.ElasticSearch.Config
{
    public class ElasticsearchConfig
    {
        public string Url { get; private set; }

        public ElasticsearchConfig(IConfigurationRoot configuration)
        {
            Url = configuration["Elasticsearch:Url"];
        }

    }
}
