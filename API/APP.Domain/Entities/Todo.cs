﻿using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Domain.Entities
{
    public class Todo : DeletedEntity
    {
        public string Title { get; set; }
    }
}
