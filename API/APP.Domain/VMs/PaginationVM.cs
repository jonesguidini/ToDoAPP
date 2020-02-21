using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class PaginationVM<TEntity> where TEntity : BaseEntity
    {
        public IList<TEntity> Data { get; set; }
        public int TotalPages { get; set; }
        public int TotalData { get; set; }

        public static PaginationVM<TEntity> Empty() => new PaginationVM<TEntity> { Data = Enumerable.Empty<TEntity>().ToList(), TotalPages = 0 };
    }
}
