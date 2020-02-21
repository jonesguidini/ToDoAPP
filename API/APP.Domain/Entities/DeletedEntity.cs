using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Domain.Entities
{
    public abstract class DeletedEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? DeletedByUserId { get; set; }
        public virtual User? DeletedByUser { get; set; }
    }
}
