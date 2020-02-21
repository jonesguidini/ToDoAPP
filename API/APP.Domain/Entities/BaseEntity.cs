using System;

namespace APP.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
    }
}
