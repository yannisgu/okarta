using ConfigR;
using Nancy;

namespace Okarta.Web
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["Index", new {DevMode = Config.Global.Get<bool>("isLocal")}];
            };
        }

    }
}