using Newtonsoft.Json;

namespace Okarta.Data.Entities
{
    public class User : Entity
    {
        public virtual string Username { get; set; }
        public virtual string EscapedPassword { get; set; }
        public virtual string Email { get; set; }
    }
}
