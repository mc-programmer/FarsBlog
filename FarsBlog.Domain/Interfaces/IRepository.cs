using FarsBlog.Domain.DTOs.ViewModels.Common;
using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;
using FarsBlog.Domain.Models.Common;
using System.Linq.Expressions;

namespace FarsBlog.Domain.Interfaces;

public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null, params string[] includeProperties);
    Task FilterAsync<TModel>(BasePaging<TModel> filterModel, FilterConditions<TEntity> filterConditions, Expression<Func<TEntity, TModel>> mapping, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null);
    Task InsertAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(TKey id, params string[] includeProperties);
    Task<TEntity?> LastOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null, params string[] includeProperties);
    void Update(TEntity entityToUpdate);
    Task<bool> AnyAsync(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate);
    Task<bool> IsValidAsync(string propertyName, string propertyValue);
    Task<bool> IsValidAsync(string propertyName, string propertyValue, TKey? id);
    void UpdateRange(List<TEntity> entitiesToUpdatee);
    Task SaveAsync();
}