using ConfigR;

namespace Okarta.Data
{
    using System.Data.Entity;
    using Entities;

    public class DataContext : DbContext
    {
        public DataContext() : base((Config.Global.Get<string>("connection")))
        {
            
        }

        public virtual DbSet<Map> Maps { get; set; }
    }
}
