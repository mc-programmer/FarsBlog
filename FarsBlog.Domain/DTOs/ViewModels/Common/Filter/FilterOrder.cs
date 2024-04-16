using System.Linq.Expressions;

namespace FarsBlog.Domain.ViewModel.Common.Filter;

public class FilterOrder<TEntity>
{
    public bool IsAscending { get; private set; }

    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    public FilterOrder(Expression<Func<TEntity, object>> orderBy, bool isAscending)
    {
        IsAscending = isAscending;
        OrderBy = orderBy;
    }
}
