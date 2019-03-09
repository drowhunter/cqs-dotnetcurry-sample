using System.Threading.Tasks;
using Cqs.CoreSampleLib.Constants;
using Cqs.CoreSampleLib.Cqs;
using Cqs.CoreSampleLib.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cqs.CoreConsole.Requests.Commands
{
    public class SaveBookCommandHandler : CommandHandler<SaveBookCommand, SaveBookCommandResult>
    {
        public SaveBookCommandHandler(ApplicationDbContext context, ILogger<SaveBookCommandHandler> logger) : base(context,logger)
        {
        }

        protected override SaveBookCommandResult DoHandle(SaveBookCommand request)
        {
            var _response = new SaveBookCommandResult();

            AddBook(request);

            //persist changes to the datastore
            ApplicationDbContext.SaveChanges();

            return _response;
        }

        protected override async Task<SaveBookCommandResult> DoHandleAsync(SaveBookCommand request)
        {
            var _response = new SaveBookCommandResult();
            //persist changes to the datastore
            AddBook(request);
                
            await    ApplicationDbContext.SaveChangesAsync();
            
            return _response;
        }

        private void AddBook(SaveBookCommand request)
        {
//attach the book
            ApplicationDbContext.Books.Attach(request.Book);

            //add or update the book entity
            ApplicationDbContext.Entry(request.Book).State =
                request.Book.Id == 0 ? EntityState.Added : EntityState.Modified;

            
        }

        
    }
}