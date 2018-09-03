using DemoElastic.ElasticSearch.Contracts;
using Nest;

namespace DemoElastic.ElasticSearch.Agenda
{
    [ElasticsearchType(Name = "agenda")]
    public class AgendaQueryModel : IQueryModel
    {
        public AgendaQueryModel() { }

        public int Id { get; set; }
        public string Nome { get; set; }
        [Keyword]
        public string Celular { get; set; }
        public string Email { get; set; }

        public string GetIndexName()
        {
            return "idx-agenda";
        }
    }
}
