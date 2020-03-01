using APP.Domain.Entities;
using APP.Domain.Filters;
using APP.Domain.VMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APP.Domain.Contracts.Services
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity obj);

        Task<TEntity> Update(TEntity obj);

        Task Delete(int id);

        Task<TEntity> DeleteLogically(TEntity obj);

        Task<TEntity> GetById(int id, bool? getDeletedRegisters = false);

        Task<TEntity> GetById(int id, IList<string> includes, bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> GetAll(bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> GetAll(IList<string> includes, bool? getDeletedRegisters = false);

        Task<IQueryable<TEntity>> GetPaginated(int page, int pageSize, bool? getDeletedRegisters = false);

        PaginationVM<MT> GetPaginated<MT>(int page, int pageSize, Expression<Func<TEntity, bool>> where = null, IList<string> includes = null, Expression<Func<TEntity, object>> orderBy = null, TypeOrderBy tipoOrderBy = TypeOrderBy.Ascending, Expression<Func<TEntity, object>> thenBy = null, bool? getDeletedRegisters = false) where MT : BaseEntity;

        PaginationVM<MT> GetPaginated<MT>(int page, int pageSize, IList<MT> data = null, bool orderByUser = false, bool? getDeletedRegisters = false) where MT : BaseEntity;

        bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity;
    }
}
