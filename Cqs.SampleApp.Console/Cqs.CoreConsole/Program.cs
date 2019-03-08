using System;
using Cqs.CoreConsole.Requests.Commands;
using Cqs.CoreConsole.Requests.Queries.Books;
using Cqs.CoreSampleLib.Cqs;
using Cqs.CoreSampleLib.DataAccess;
using Cqs.CoreSampleLib.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cqs.CoreConsole
{
    class Program
    {
        private static ILogger<Program> _logger;
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Database=cqs_books_test; Trusted_Connection=True;MultipleActiveResultSets=true";// Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                    options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                })


                .AddScoped<IQueryDispatcher, QueryDispatcher>()
                .AddScoped<ICommandDispatcher, CommandDispatcher>()

                .AddLogging(builder => builder.AddConsole());

            serviceCollection.Scan(scan =>
            {
                scan.FromAssemblyOf<ApplicationDbContext>()
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
            serviceCollection.Scan(scan =>
            {
                scan.FromAssemblyOf<ApplicationDbContext>()
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            var serviceProvider= serviceCollection.BuildServiceProvider();
            

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            logger.LogDebug("Bootsrapping Application");

            WithCqsAsync(serviceProvider);

            Console.ReadKey();

        }

        private static async void WithCqsAsync(IServiceProvider service)
        {
            var _commandDispatcher = service.GetService<ICommandDispatcher>();
            var _queryDispatcher = service.GetService<IQueryDispatcher>();

            var _response = await _queryDispatcher.DispatchAsync<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            _logger.LogInformation("Retrieving all books the CQS Way..");

            foreach (var _book in _response.Books)
            {
                _logger.LogInformation("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }

            //edit first book
            var _bookToEdit = _response.Books.First();
            _bookToEdit.InMyPossession = !_bookToEdit.InMyPossession;
            await _commandDispatcher.DispatchAsync<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = _bookToEdit
            });


            //add new book
            await _commandDispatcher.DispatchAsync<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = new Book()
                {
                    Title = "C# in Depth",
                    Authors = "Jon Skeet",
                    InMyPossession = false,
                    DatePublished = new DateTime(2013, 07, 01)
                }
            });


            _response = await _queryDispatcher.DispatchAsync<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            foreach (var _book in _response.Books)
            {
                _logger.LogInformation("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }

        }
    }
}
