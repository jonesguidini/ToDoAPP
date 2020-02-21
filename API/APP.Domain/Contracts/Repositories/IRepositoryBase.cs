using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using APP.Domain.Entities;

namespace APP.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity obj);

        Task<TEntity> Update(TEntity obj);

        Task Delete(TEntity obj);

        Task<TEntity> GetById(int id, bool? getDeletedRegisters = false);

        Task<TEntity> GetById(int id, IList<string> includes, bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> GetAll(bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> GetAll(IList<string> includes, bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate, bool? getDeletedRegisters = false);

        Task SaveChanges();
    }
}
