using System.Linq.Expressions;

namespace FarsBlog.Domain.ViewModel.Common.Filter
{
    public static class Filter
    {
        public static FilterOrder<TEntity> OrderBy<TEntity>(Expression<Func<TEntity, object>> orderBy, bool isAscending)
        {
            return new FilterOrder<TEntity>(orderBy, isAscending);
        }

        public static FilterConditions<TEntity> GenerateConditions<TEntity>()
        {
            FilterConditions<TEntity> filterConditions = new FilterConditions<TEntity>();
            return filterConditions;
        }
    }
}
