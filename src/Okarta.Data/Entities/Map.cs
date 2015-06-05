namespace Okarta.Data.Entities
{
    public class Map : Entity
    {
        public virtual string Name { get; set; }
        public virtual float? Lat { get; set; }
        public virtual float? Lon { get; set; }
    }
}
