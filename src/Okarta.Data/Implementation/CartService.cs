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
        public IEnumerable<CartItem> GetCartForUser(User user)
        {
            return Session.Query<CartItem>()
            .Where(_ => _.User.Id.ToString() == user.Id.ToString()).ToList();
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