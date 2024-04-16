using FarsBlog.Domain.Interfaces;
using FarsBlog.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FarsBlog.Infra.Data.Repositories.Common;

public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    #region Fields

    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    #endregion

    #region Constructor

    public EfRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    #endregion

    #region Methods

    public virtual async Task<TEntity?> LastOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null, params string[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (string inlcudeProperty in includeProperties)
        {
            query = query.Include(inlcudeProperty);
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        if (orderByDesc != null)
        {
            query = query.OrderByDescending(orderByDesc);
        }

        return await query.LastOrDefaultAsync();
    }
    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null, params string[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (string includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        if (orderByDesc != null)
        {
            query = query.OrderByDescending(orderByDesc);
        }

        return await query.ToListAsync();
    }
    //public virtual async Task FilterAsync<TModel>(BasePaging<TModel> filterModel, FilterConditions<TEntity> filterConditions, Expression<Func<TEntity, TModel>> mapping, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null)
    //{
    //    IQueryable<TEntity> query = _dbSet;

    //    foreach (Expression<Func<TEntity, bool>> filter2 in filterConditions)
    //    {

    //        query = query.Where(filter2);

    //    }

    //    if (orderBy != null)
    //    {
    //        query = query.OrderBy(orderBy);
    //    }
    //    else if (orderByDesc != null)
    //    {
    //        query = query.OrderByDescending(orderByDesc);
    //    }
    //    else if (typeof(TEntity)!.IsAssignableTo(typeof(BaseEntity<TKey>)))
    //    {
    //        query = query.OrderByDescending((TEntity entity) => (entity as BaseEntity<TKey>).CreateDateOnUtc);
    //    }

    //    await filterModel.Paging(query.Select(mapping));
    //}

    public virtual async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public virtual async Task<TEntity?> GetByIdAsync(TKey id, params string[] includeProperties)
    {
        TKey id2 = id;
        if (id2 == null)
        {
            return null;
        }

        IQueryable<TEntity> query = _dbSet.AsQueryable();
        foreach (string inlcudeProperty in includeProperties)
        {
            query = query.Include(inlcudeProperty);
        }

        return await query.FirstOrDefaultAsync((TEntity entity) => entity.Id.Equals(id2));
    }
    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Update(entityToUpdate);
    }
    public virtual void UpdateRange(List<TEntity> entitiesToUpdatee)
    {
        _dbSet.UpdateRange(entitiesToUpdatee);
    }
    public virtual async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}