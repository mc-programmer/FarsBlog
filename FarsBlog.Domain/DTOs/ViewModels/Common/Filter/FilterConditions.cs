using System.Linq.Expressions;

namespace FarsBlog.Domain.ViewModel.Common.Filter;

public class FilterConditions<TEntity> : List<Expression<Func<TEntity, bool>>>
{
}
