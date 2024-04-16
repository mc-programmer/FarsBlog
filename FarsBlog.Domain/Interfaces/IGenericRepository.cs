using System.Linq.Expressions;

namespace FarsBlog.Domain.Interfaces;

public interface IGenericRepository<TEntity, TKey> where TEntity : class where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task InsertAsync(TEntity model);
    Task UpdateAsync(TKey id, TEntity model);
    Task DeleteAsync(TKey id);
}

//public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where 
//{
//    public Task DeleteAsync(TKey id)
//    {
//        throw new NotImplementedException();
//    }

//    public IQueryable<TEntity> GetAll()
//    {
//        throw new NotImplementedException();
//    }

//    public Task<TEntity?> GetByIdAsync(TKey id)
//    {
//        throw new NotImplementedException();
//    }

//    public Task InsertAsync(TEntity model)
//    {
//        throw new NotImplementedException();
//    }

//    public Task UpdateAsync(TKey id, TEntity model)
//    {
//        throw new NotImplementedException();
//    }
//}