using System;
using System.Linq;
using Codenation.Challenge.Models;

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

        public Quote GetAnyQuote()
        {
            int quote = _context.Quotes.ToList().Count();

            // valido antes se a lista está vazia
            if(quote == 0)
            {
                return null;
            }
            else
            {
                int index = _randomService.RandomInteger(quote);

                var result = _context.Quotes.Where(x => x.Actor != null).SkipLast(index).FirstOrDefault();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            
        }

        public Quote GetAnyQuote(string actor)
        {
            int quote = _context.Quotes.ToList().Count();

            // valido se a lista está vazia antes
            if (quote == 0)
            {
                return null;
            }
            else
            {
                int index = _randomService.RandomInteger(quote);

                var result = _context.Quotes.Where(x => x.Actor == actor)
                    .Skip(index)
                    .FirstOrDefault();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}