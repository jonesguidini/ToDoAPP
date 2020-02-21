using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Domain.DTOs
{
    public class DeletedEntityDTO
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? DeletedByUserId { get; set; }
    }
}
