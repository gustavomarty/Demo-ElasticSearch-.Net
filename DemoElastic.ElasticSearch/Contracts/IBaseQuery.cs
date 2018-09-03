using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoElastic.ElasticSearch.Contracts
{
    /// <summary>
    /// Base interface to queries
    /// </summary>
    /// <typeparam name="TModel">Complete storaged model</typeparam>
    /// <typeparam name="TIndexModel">Model with the properties that should be indexed</typeparam>
    public interface IBaseQuery<TModel, TIndexModel>
    {
        /// <summary>
        /// Adds the <typeparamref name="TModel"/> document to index
        /// </summary>
        /// <param name="model">Document to be indexed</param>
        /// <returns></returns>
        Task<bool> IndexAsync(TModel document);

        /// <summary>
        /// Create mapping
        /// </summary>
        /// <returns></returns>
        Task CreateIndexAsync();

    }

    /// <summary>
    /// Base interface to queries
    /// </summary>
    /// <typeparam name="TModel">The model to be indexed</typeparam>
    public interface IBaseQuery<TModel> : IBaseQuery<TModel, TModel> { }
}
