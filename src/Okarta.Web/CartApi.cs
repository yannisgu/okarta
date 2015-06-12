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
                return cartService.GetCartForUser(User);
            };


            Post["/"] = _ =>
            {
                var cartItem = JsonConvert.DeserializeObject<CartItem>(Request.Body.AsString());
                cartItem.UserId = User.Id;
                cartService.Add(cartItem);
                return null;
            };
        }
    }
}