using Nancy;
using Okarta.Data;
using Okarta.Data.Entities;

namespace Okarta.Web
{
    public abstract class BaseApiModule : NancyModule
    {
        protected readonly IUserService userService;

        public User User
        {
            get
            {
                if (string.IsNullOrEmpty(Context.CurrentUser.UserName))
                {
                    return null;
                }
                return userService.Get(Context.CurrentUser.UserName);
            }
        }

        public BaseApiModule(string url, IUserService userService) : base(url)
        {
            this.userService = userService;
        }
    }
}