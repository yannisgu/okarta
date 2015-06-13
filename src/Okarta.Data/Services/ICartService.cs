using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okarta.Data.Entities;

namespace Okarta.Data.Services
{
    public interface ICartService
    {
        IEnumerable<CartItem> GetCartForUser(User user);
        void Add(CartItem cartItem);
    }
}
