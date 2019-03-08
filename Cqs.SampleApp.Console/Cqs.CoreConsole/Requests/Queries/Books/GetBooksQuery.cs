﻿using System.Collections.Generic;
using Cqs.CoreSampleLib.Cqs.Data;
using Cqs.CoreSampleLib.Domain;

namespace Cqs.CoreConsole.Requests.Queries.Books
{
    public class GetBooksQuery : Query
    {
        public bool ShowOnlyInPossession { get; set; }
        
        //other filters here
    }

    public class GetBooksQueryResult : IResult
    {
        public IEnumerable<Book> Books { get; set; }
    }
}