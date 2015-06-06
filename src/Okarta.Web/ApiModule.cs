using System;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using Okarta.Data;
using Nancy.Authentication.Token;
using Nancy.Security;

namespace Okarta.Web
{
    public class MapsModule : NancyModule
    {
        public MapsModule(ITokenizer tokenizer)
            : base("/api")
        {
            new MapsApiDefinition().RegisterNancyModule(this);

            Post["/login"] = _ =>
            {
                var user = JsonConvert.DeserializeObject<LoginViewModel>(Context.Request.Body.AsString());
                var userIdentity = new UserService().ValidatedUser(user.Username, user.Password);
                if (userIdentity == null)
                {
                    return HttpStatusCode.Unauthorized;
                }
                var token = tokenizer.Tokenize(userIdentity.Identity(), Context);
                return new
                {
                    Token = token,
                };
            };

            Get["/me"] = _ =>
            {
                this.RequiresAuthentication();
                return Context.CurrentUser;
            };
        }

    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
