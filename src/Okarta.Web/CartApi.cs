using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Extensions;
using Newtonsoft.Json;
using Okarta.Data;
using Okarta.Data.Entities;
using Okarta.Data.Services;

namespace Okarta.Web
{
    public class CartApi : BaseApiModule
    {
        public CartApi(IUserService userService, ICartService cartService) : base("/api/cart", userService)
        {
            Get["/"] = _  => 
            {
                var maps = cartService.GetCartForUser(User);
                return
                    maps.Select(m =>
                    {
                        m.User = null;
                        return m;
                    });
            };


            Post["/"] = _ =>
            {
                var cartItem = JsonConvert.DeserializeObject<CartItem>(Request.Body.AsString());
                cartItem.User = User;
                cartService.Add(cartItem);
                return null;
            };
        }
    }
}