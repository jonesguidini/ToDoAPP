using System;
using System.Collections.Generic;
using System.Text;
using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class DeletedEntityVM : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? DeletedByUserId { get; set; }
        public string DeletedByUser { get; set; }
    }
}
