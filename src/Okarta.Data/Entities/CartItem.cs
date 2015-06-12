using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okarta.Data.Entities
{
    public class CartItem : Entity 
    {
        public virtual int Amount { get; set; }
        public virtual Guid MapId { get; set; }
        public virtual Map Map { get; set; }
        public virtual MapBuyType Type { get; set; }
        public virtual Guid UserId { get; set; }
    }

    public enum MapBuyType
    {
        Delivered = 1,
        Download = 2
    }
}
