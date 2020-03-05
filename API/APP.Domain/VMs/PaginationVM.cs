using APP.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace APP.Domain.VMs
{
    public class PaginationVM<TEntity> where TEntity : BaseEntity
    {
        public IList<TEntity> PaginatedResult { get; set; }
        public int TotalPages { get; set; }
        public int TotalData { get; set; }

        public static PaginationVM<TEntity> Empty() => new PaginationVM<TEntity> { PaginatedResult = Enumerable.Empty<TEntity>().ToList(), TotalPages = 0 };
    }
}
