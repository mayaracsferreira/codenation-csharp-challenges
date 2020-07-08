using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class QuoteService : IQuoteService
    {
        private ScriptsContext _context;
        private IRandomService _randomService;

        public QuoteService(ScriptsContext context, IRandomService randomService)
        {
            this._context = context;
            this._randomService = randomService;
        }

        public List<Quote> GetAllQuotes()
        {
            var result = _context.Quotes.ToList();

            return result;

        }
        public Quote GetAnyQuote()
        {
            var count = _context.Quotes.Count();

            var randomNumber = _randomService.RandomInteger(count);

            var result = _context.Quotes
                .Skip(randomNumber)
                .FirstOrDefault();

            return result;
        }

        public Quote GetAnyQuote(string actor)
        {
            var count = _context.Quotes.Count();

            var randomNumber = _randomService.RandomInteger(count);

            var result = _context.Quotes
                .Where(_ => _.Actor == actor)
                .Skip(randomNumber)
                .FirstOrDefault();

            return result;
        }
    }
}