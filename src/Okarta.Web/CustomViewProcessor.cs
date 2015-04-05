using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using Nancy.Responses.Negotiation;
using Nancy.ViewEngines;

namespace Okarta.Web
{
    public class CustomViewProcessor : IResponseProcessor
    {
        private ViewProcessor viewProcessor;

        public CustomViewProcessor(IViewFactory viewFactory)
        {
            viewProcessor = new ViewProcessor(viewFactory);
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            if (context.Request.Url.Path == "/")
            {
                return new ProcessorMatch()
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.ExactMatch
                };
            }
            return new ProcessorMatch();
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return viewProcessor.Process(requestedMediaRange, model, context);
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get { return viewProcessor.ExtensionMappings; }
        }
    }
}
