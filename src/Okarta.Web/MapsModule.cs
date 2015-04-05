using System;
using ConfigR;
using Nancy;

namespace Okarta.Web
{
    public class MapsModule : NancyModule
    {
        public MapsModule() : base("/api")
        {
            new MapsApiDefinition().RegisterNancyModule(this);
        }
    }
}