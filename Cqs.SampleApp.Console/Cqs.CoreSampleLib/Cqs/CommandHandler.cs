using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs.Data;
using Cqs.CoreSampleLib.DataAccess;
using Microsoft.Extensions.Logging;

namespace Cqs.CoreSampleLib.Cqs
{
    public abstract class CommandHandler<TRequest, TResult> : ICommandHandler<TRequest, TResult>
        where TRequest : ICommand
        where TResult : IResult, new()
    {
        
        protected ApplicationDbContext ApplicationDbContext;
        private readonly ILogger<CommandHandler<TRequest, TResult>> _logger;

        protected CommandHandler(ApplicationDbContext context, ILogger<CommandHandler<TRequest, TResult>> logger)
        {
            ApplicationDbContext = context;
            _logger = logger;
           
        }


        public TResult Handle(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();
            
            TResult _response;

            try
            {
                //do data validation
                //do authorization

                _response = DoHandle(command);
            }
            catch (Exception _exception)
            {
                _logger.LogError("Error in {0} CommandHandler. Message: {1} \n Stacktrace: {2}", typeof(TRequest).Name, _exception.Message, _exception.StackTrace);

                throw;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TRequest).Name, _stopWatch.ElapsedMilliseconds);
            }

            return _response;
        }

        public async Task<TResult> HandleAsync(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TResult> _response;

            try
            {
                //do data validation
                //do authorization

                _response = DoHandleAsync(command);
            }
            catch (Exception _exception)
            {
                _logger.LogError("Error in {0} CommandHandler. Message: {1} \n Stacktrace: {2}", typeof(TRequest).Name, _exception.Message, _exception.StackTrace);

                throw;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TRequest).Name, _stopWatch.ElapsedMilliseconds);
            }

            return await _response;
        }

        // Protected methods
        protected abstract TResult DoHandle(TRequest request);

        protected abstract Task<TResult> DoHandleAsync(TRequest request);
    }
}