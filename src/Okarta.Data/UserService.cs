using System.Linq;
using System.Security.Cryptography;
using NHibernate.Linq;
using Okarta.Data.Entities;
using Okarta.Data.Implementation;

namespace Okarta.Data
{
    public class UserService : DataService, IUserService
    {
        public User ValidatedUser(string username, string password)
        {
            var user = Get(username);

            if (user != null && new PasswordService().ValidatePassword(password, user.EscapedPassword))
            {
                return user;
            }
            return null;
        }

        public User Get(string userName)
        {
            return Session.Query<User>().FirstOrDefault(_ => _.Username == userName);
        }

        public UserService(SessionProvider provider) : base(provider)
        {
        }
    }

    public interface IUserService
    {
        User ValidatedUser(string username, string password);
        User Get(string userName);
    }
}
