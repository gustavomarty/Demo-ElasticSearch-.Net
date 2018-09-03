using AutoMapper;
using DemoElastic.ElasticSearch.Base;
using DemoElastic.ElasticSearch.Config;
using DemoElastic.Model.Agenda;
using Elasticsearch.Net;
using Nest;
using System;
using System.Threading.Tasks;

namespace DemoElastic.ElasticSearch.Agenda
{
    public class AgendaQuery : BaseQuery<AgendaContatos2, AgendaQueryModel>
    {
        public AgendaQuery() : base() { }

    }
}
