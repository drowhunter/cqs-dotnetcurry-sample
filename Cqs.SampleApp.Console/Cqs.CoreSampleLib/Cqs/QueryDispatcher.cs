using System;
using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Cqs.CoreSampleLib.Cqs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _services;

        public QueryDispatcher(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult
        {
            //Look up the correct QueryHandler in our IoC container and invoke the retrieve method

            var _handler = _services.GetService<IQueryHandler<TParameter, TResult>>();
            return _handler.Retrieve(query);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult
        { 
            //Look up the correct QueryHandler in our IoC container and invoke the retrieve method

            var _handler = _services.GetService<IQueryHandler<TParameter, TResult>>();
            return await _handler.RetrieveAsync(query);
        }
    }
}