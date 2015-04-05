using System;

namespace Okarta.Data.Entities
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; } 
    }
}