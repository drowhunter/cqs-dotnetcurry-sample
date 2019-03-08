using System;
using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Cqs.CoreSampleLib.Cqs
{
    /// <inheritdoc />
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _Context;

        public CommandDispatcher(IServiceProvider context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult
        {
            //Look up the correct CommandHandler in our IoC container and invoke the Handle method

            var _handler = _Context.GetService<ICommandHandler<TParameter, TResult>>();
            return _handler.Handle(command);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult
        {
            //Look up the correct CommandHandler in our IoC container and invoke the async Handle method

            var _handler = _Context.GetService<ICommandHandler<TParameter, TResult>>();
            return await _handler.HandleAsync(command);
        }
    }
}