using APP.Business.Config;
using APP.Data.Context;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APP.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : Notifiable, IRepositoryBase<TEntity> where TEntity : DeletedEntity, new()
    {
        private readonly DBContext dbContext;
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Construtor de contexto do banco de dados
        /// </summary>
        /// <param name="_dbContext"></param>
        public RepositoryBase(DBContext _dbContext, INotificationManager _notificationManager) : base(_notificationManager)
        {
            dbContext = _dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Método padrão para Adicionar e salvar entidades no BD
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Add(TEntity obj)
        {
            DbSet.Add(obj);
            await SaveChanges();
            return obj;
        }

        /// <summary>
        /// Método padrão para Atualizar entidade no BD
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Update(TEntity obj)
        {
            dbContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await SaveChanges();
            return obj;
        }

        /// <summary>
        /// Método padrão para Remover entidade no BD
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task Delete(TEntity obj)
        {
            DbSet.Remove(await GetById(obj.Id));
            await SaveChanges();
        }

        /// <summary>
        /// Método que retorna registro de entidade filtrada por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(int id, bool? getDeletedRegisters = false)
        {
            return await Task.Run(() => FindAsync(x => x.Id == id && x.IsDeleted == getDeletedRegisters, null).Result.SingleOrDefault());
        }

        /// <summary>
        /// Método que retorna registro de entidade com respectivos includes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(int id, IList<string> includes, bool? getDeletedRegisters = false)
        {
            IQueryable<TEntity> entidades = DbSet;

            foreach (var include in includes)
                entidades = entidades.Include(include);

            return await Task.Run(() => entidades.Where(x => x.Id == id && x.IsDeleted == getDeletedRegisters).AsQueryable().FirstOrDefaultAsync());
        }

        /// <summary>
        /// Método padrão para retornar todas entidades do BD
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> GetAll(bool? getDeletedRegisters = false)
        {
            return await FindAsync(x => x.Id != 0 && x.IsDeleted == getDeletedRegisters, null);
        }

        public virtual async Task<IQueryable<TEntity>> GetAll(IList<string> includes, bool? getDeletedRegisters = false)
        {
            IQueryable<TEntity> entidades = DbSet;

            foreach (var include in includes)
                entidades = entidades.Include(include);

            return await Task.Run(() => entidades.Where(x => x.IsDeleted == getDeletedRegisters).AsQueryable());
        }

        /// <summary>
        /// Método para efetuar filtro em registros de entidades
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate, bool? getDeletedRegisters = false)
        {
            var registers = await FindAsync(predicate).Result.Where(x => x.IsDeleted == getDeletedRegisters).ToListAsync();
            return await Task.Run(() => registers.AsQueryable());
        }

        /// <summary>
        /// Efetua commit para save no banco de dados 
        /// </summary>
        /// <returns></returns>
        public async Task SaveChanges()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                await Notify("Error", message);
            }
        }

        /// <summary>
        /// Método padrão para filtrar registros passando entidade e opcionamente includes (objeto) para consulta
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.Where(predicate).AsQueryable();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        /// <summary>
        /// Método padrão que complementa o método Find para trazer de modo Assincrono registros em formato QueryRiable
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Task.Run(() => Find(predicate, includes));
        }
    }
}
