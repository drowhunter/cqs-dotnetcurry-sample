using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs.Data;

namespace Cqs.CoreSampleLib.Cqs
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<in TParameter, TResult> where TParameter : ICommand
        where TResult : IResult
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        TResult Handle(TParameter command);

        /// <summary>
        /// Executes an async command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        Task<TResult> HandleAsync(TParameter command);
    }
}