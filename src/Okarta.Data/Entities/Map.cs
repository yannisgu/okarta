namespace Okarta.Data.Entities
{
    public class Map : Entity
    {
        public string Name { get; set; }
        public float? Lat { get; set; }
        public float? Lon { get; set; }
    }
}