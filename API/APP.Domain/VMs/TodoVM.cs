using System;
using System.Collections.Generic;
using System.Text;
using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class TodoVM : BaseEntity
    {
        public string Title { get; set; }
    }
}
