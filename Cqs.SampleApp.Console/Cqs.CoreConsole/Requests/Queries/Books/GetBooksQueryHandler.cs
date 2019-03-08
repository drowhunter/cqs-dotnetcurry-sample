using System.Linq;
using System.Threading.Tasks;
using Cqs.CoreSampleLib.Cqs;
using Cqs.CoreSampleLib.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cqs.CoreConsole.Requests.Queries.Books
{
    public class GetBooksQueryHandler : QueryHandler<GetBooksQuery, GetBooksQueryResult>
    {
        public GetBooksQueryHandler(ApplicationDbContext applicationDbContext, ILogger<GetBooksQueryHandler> logger) 
            : base(applicationDbContext, logger)
        {
        }

        protected override GetBooksQueryResult Handle(GetBooksQuery request)
        {
            var _result = new GetBooksQueryResult();

            var _bookQuery = ApplicationDbContext.Books.AsQueryable();

            if (request.ShowOnlyInPossession)
            {
                _bookQuery = _bookQuery.Where(c => c.InMyPossession);
            }

            _result.Books = _bookQuery.ToList();

            return _result;
        }

        protected override async Task<GetBooksQueryResult>  HandleAsync(GetBooksQuery request)
        {
            var _result = new GetBooksQueryResult();

            var _bookQuery = await ApplicationDbContext.Books.ToListAsync();

            if (request.ShowOnlyInPossession)
            {
                _bookQuery = _bookQuery.Where(c => c.InMyPossession).ToList();
            }

            _result.Books = _bookQuery.ToList();

            return _result;
        }
    }
}