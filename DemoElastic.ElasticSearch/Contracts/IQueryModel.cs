using System;
using System.Collections.Generic;
using System.Text;

namespace DemoElastic.ElasticSearch.Contracts
{
    public interface IQueryModel
    {
        string GetIndexName();
    }
}
