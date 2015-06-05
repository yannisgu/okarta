using System.Collections.Generic;
using Nancy.Security;
using NHibernate.Mapping;
using Okarta.Data.Entities;

namespace Okarta.Web
{
    public class UserIdentity : IUserIdentity
    {
        public User User { get; set; }

        public UserIdentity(User user)
        {
            User = user;
        }

        public string UserName { get { return User.Username; } }

        public IEnumerable<string> Claims
        {
            get { return new List<string>(); }
        }
    }

    public static class UserExtensions
    {
        public static UserIdentity Identity(this User user)
        {
            return new UserIdentity(user);
        }
    }
}