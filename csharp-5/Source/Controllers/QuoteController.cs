using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _service;

        public QuoteController(IQuoteService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public ActionResult<List<Quote>> GetAllQuotes()
        {
            var result = _service.GetAllQuotes();
            return Ok(result);
        }

        // GET api/quote
        [HttpGet]
        public ActionResult<QuoteView> GetAnyQuote()
        {
            var result = _service.GetAnyQuote();

            if (result == null)
                return NotFound();

            return new QuoteView()
            {
                Id = result.Id,
                Actor = result.Actor,
                Detail = result.Detail
            };
        }

        // GET api/quote/{actor}
        [HttpGet("{actor}")]
        public ActionResult<QuoteView> GetAnyQuote(string actor)
        {

            var result = _service.GetAnyQuote(actor);

            if (result != null)
            {
                QuoteView quote = new QuoteView();
                quote.Id = result.Id;
                quote.Actor = result.Actor;
                quote.Detail = result.Detail;
                return quote;
            }
            return new NotFoundResult();
        }

    }
}
