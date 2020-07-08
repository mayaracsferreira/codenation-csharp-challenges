
using Codenation.Challenge.Models;
using System.Collections.Generic;

namespace Codenation.Challenge.Services
{
    public interface IQuoteService
    {
        List<Quote> GetAllQuotes();
        Quote GetAnyQuote();
        Quote GetAnyQuote(string actor);
    }
}
