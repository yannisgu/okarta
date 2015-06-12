using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Okarta.Data.Entities;
using Okarta.Data.Services;

namespace Okarta.Data.Implementation
{
    public class CartService : DataService, ICartService
    {
        public IList<CartItem> GetCartForUser(User user)
        {
            return Session.Query<CartItem>().Where(_ => _.UserId == user.Id).Fetch(_ => _.Map).ToList();
        }

        public void Add(CartItem cartItem)
        {
            cartItem.Id = Guid.NewGuid();
            Session.Save(cartItem);
            Session.Flush();
        }

        public CartService(SessionProvider provider) : base(provider)
        {
        }
    }
}