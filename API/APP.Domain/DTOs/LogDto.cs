using System;
using APP.Domain.Entities;

namespace APP.Domain.DTOs
{
    public class LogDTO : BaseEntity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}