using System;
using System.Collections.Generic;
using System.Text;
using APP.Domain.Entities;

namespace APP.Domain.DTOs
{
    public class TodoDto : BaseEntity
    {
        public string Title { get; set; }
    }
}
