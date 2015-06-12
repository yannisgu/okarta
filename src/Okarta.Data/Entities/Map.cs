using System;

namespace Okarta.Data.Entities
{
    public class Map : Entity
    {
        public virtual string Name { get; set; }
        public virtual float? Lat { get; set; }
        public virtual float? Lon { get; set; }

        public virtual decimal? DownloadPrice { get; set; }

        public virtual User Owner { get; set; }
        public virtual Guid OwnerId { get; set; }
    }
}
