using System.Collections.Generic;
using Newtonsoft.Json;

namespace Okarta.Data.Entities
{
    public class User : Entity
    {
        public virtual string Username { get; set; }
        public virtual string EscapedPassword { get; set; }
        public virtual string Email { get; set; }

        public virtual IList<CartItem> CartItems { get; set; } 
    }
}
