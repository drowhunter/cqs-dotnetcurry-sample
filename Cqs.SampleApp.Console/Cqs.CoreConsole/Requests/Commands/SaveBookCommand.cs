﻿namespace Cqs.CoreConsole.Requests.Commands
{
    public class SaveBookCommand : Command
    {
        public Book Book { get; set; }
    }

    public class SaveBookCommandResult : IResult
    {
    }
}