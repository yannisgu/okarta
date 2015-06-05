using System.Linq;
using System.Security.Cryptography;
using NHibernate.Linq;
using Okarta.Data.Entities;

namespace Okarta.Data
{
    public class UserService : IUserService
    {
        public User ValidatedUser(string username, string password)
        {
            using (var session = new SessionProvider().GetSessionFactory().OpenSession())
            {
                var user =
                    session.Query<User>().FirstOrDefault(_ => _.Username == username);

                if (user != null && new PasswordService().ValidatePassword(password, user.EscapedPassword))
                {
                    return user;
                }
                return null;
            }
        }
    }

    public interface IUserService
    {
        User ValidatedUser(string username, string password);
    }
}
