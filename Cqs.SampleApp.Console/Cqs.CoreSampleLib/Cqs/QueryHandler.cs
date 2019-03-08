using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs.Data;
using Cqs.CoreSampleLib.DataAccess;
using Microsoft.Extensions.Logging;

namespace Cqs.CoreSampleLib.Cqs
{
    public abstract class QueryHandler<TParameter, TResult> : IQueryHandler<TParameter, TResult>
        where TResult : IResult, new()
        where TParameter : IQuery, new()
    {
        
        protected ApplicationDbContext ApplicationDbContext;
        private readonly ILogger<QueryHandler<TParameter, TResult>> _logger;

        protected QueryHandler(ApplicationDbContext applicationDbContext, ILogger<QueryHandler<TParameter, TResult>> logger)
        {
            ApplicationDbContext = applicationDbContext;
            _logger = logger;
            
        }

        public TResult Retrieve(TParameter query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            TResult _queryResult;

            try
            {
                //do authorization and validatiopn

                //handle the query request
                _queryResult = Handle(query);
                
            }
            catch (Exception _exception)
            {
                _logger.LogError("Error in {0} queryHandler. Message: {1} \n Stacktrace: {2}", typeof(TParameter).Name, _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TParameter).Name, _stopWatch.ElapsedMilliseconds);
            }


            return _queryResult;
        }

        public async Task<TResult> RetrieveAsync(TParameter query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TResult> _queryResult;

            try
            {
                //do authorization and validatiopn

                //handle the query request
                _queryResult = HandleAsync(query);

            }
            catch (Exception _exception)
            {
                _logger.LogError("Error in {0} queryHandler. Message: {1} \n Stacktrace: {2}", typeof(TParameter).Name, _exception.Message, _exception.StackTrace);
                //Do more error more logic here
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TParameter).Name, _stopWatch.ElapsedMilliseconds);
            }


            return await _queryResult;
        }

        /// <summary>
        /// The actual Handle method that will be implemented in the sub class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract TResult Handle(TParameter request);

        /// <summary>
        /// The actual async Handle method that will be implemented in the sub class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResult> HandleAsync(TParameter request);

    }
}