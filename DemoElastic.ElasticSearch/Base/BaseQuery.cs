using AutoMapper;
using DemoElastic.ElasticSearch.Contracts;
using Elasticsearch.Net;
using Nest;
using System;
using System.Threading.Tasks;

namespace DemoElastic.ElasticSearch.Base
{
    public abstract class BaseQuery<TModel, TIndexModel> : IBaseQuery<TModel, TIndexModel>
                where TModel : class
                where TIndexModel : class
    {
        #region [ ATTRIBUTES ]
        public const int MAX_QUERY_SIZE = 10000;

        protected readonly IMapper _mapper;
        private ElasticClient _client = null;
        #endregion [ ATTRIBUTES ]

        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="config">Elastic Search Configuration</param>
        /// <param name="mapper">AutoMapper</param>
        public BaseQuery()
        {
            var configMapping = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModel, TIndexModel>();
            });
            _mapper = configMapping.CreateMapper();
        }

        #endregion [ CONSTRUCTORS ]

        #region [ PROPERTIES ]
        /// <summary>
        /// Elastic Client
        /// </summary>
        internal ElasticClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = GetClient();
                }

                return _client;
            }
        }

        /// <summary>
        /// Get default index name
        /// </summary>
        private string DefaultIndex
        {
            get
            {
                IQueryModel instance = (IQueryModel)Activator.CreateInstance(typeof(TIndexModel), true);
                return instance.GetIndexName();
            }
        }

        /// <inheritdoc />
        public int MaxPageSize
        {
            get { return MAX_QUERY_SIZE; }
        }

        #endregion [ PROPERTIES ]

        #region [ METHODS ]
        /// <summary>
        /// Gets the 
        /// </summary>
        /// <returns></returns>
        internal ElasticClient GetClient()
        {
            if (_client != null)
                return _client;

            var uris = new[]
            {
                new Uri("[INSERT HERE YOUR ELASTICSEARCH URL]")
                #error ENTER WITH YOUR ELASTICSEARCH URL
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var connection = new HttpConnection();
            var connectionSettings = new ConnectionSettings(connectionPool, connection).DefaultIndex(DefaultIndex);

#if DEBUG
            connectionSettings.EnableDebugMode().PrettyJson(); //Debugging
#endif

            connectionSettings.DisablePing();
            connectionSettings.SniffOnStartup(false);

            _client = new ElasticClient(connectionSettings);
            return _client;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAllAsync()
        {
            await Client.DeleteByQueryAsync<TIndexModel>(d => d.MatchAll());
            await Client.ForceMergeAsync(DefaultIndex); // Force Merge to free space
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> IndexAsync(TModel document)
        {
            var indexModel = _mapper.Map<TIndexModel>(document);
            var res = await Client.IndexAsync(indexModel);
            return true;
        }

        /// <inheritdoc />
        public async Task CreateIndexAsync()
        {
            IQueryModel instance = (IQueryModel)Activator.CreateInstance(typeof(TIndexModel), true);
            string _idx_name = instance.GetIndexName();
            IndexName indexName = new IndexName() { Name = _idx_name };

            var indexExistsRequest = new IndexExistsRequest(indexName);
            var existsResult = await GetClient().IndexExistsAsync(indexExistsRequest);

            if (existsResult.Exists)
                await GetClient().DeleteIndexAsync(indexName);


            var createIndexResponse = await GetClient().CreateIndexAsync(indexName, c => c
                                                                                          .Mappings(ms => ms
                                                                                           .Map<TIndexModel>(m => m
                                                                                            .AutoMap()))
                                                                                           .Settings(s => s
                                                                                             .Analysis(a => a
                                                                                               .Analyzers(an => an
                                                                                                 .Custom("default", ca => ca
                                                                                                   .Tokenizer("lowercase")
                                                                                                   .Filters("asciifolding"))))));
        }
        #endregion [ METHODS ]

    }
}