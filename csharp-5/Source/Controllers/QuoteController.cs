using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/quote
        [HttpGet]
        public ActionResult<QuoteView> GetAnyQuote()
        {
            var itemAnyQuote = _service.GetAnyQuote();

            if (itemAnyQuote != null)
            {
                var responseQuoteView = new QuoteView()
                {
                    Id = itemAnyQuote.Id,
                    Actor = itemAnyQuote.Actor,
                    Detail = itemAnyQuote.Detail

                };

                return responseQuoteView;

            }
            else
            {
                return NotFound();
            }
        }

        // GET api/quote/{actor}
        [HttpGet("{actor}")]
        public ActionResult<QuoteView> GetAnyQuote(string actor)
        {
            var itemAnyQuote = _service.GetAnyQuote(actor);

            if (itemAnyQuote != null)
            {
                var responseQuoteView = new QuoteView()
                {
                    Id = itemAnyQuote.Id,
                    Actor = itemAnyQuote.Actor,
                    Detail = itemAnyQuote.Detail

                };

                return responseQuoteView;

            }
            else
            {
                return NotFound();
            }
        }

    }
}