﻿using System.Threading.Tasks;

namespace Cqs.CoreConsole.Requests.Queries.Books
{
    public class GetBooksQueryHandler : QueryHandler<GetBooksQuery, GetBooksQueryResult>
    {
        public GetBooksQueryHandler(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
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