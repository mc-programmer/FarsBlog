using System.Linq.Expressions;

namespace FarsBlog.Domain.DTOs.ViewModels.Common.Filter;

public class FilterConditions<TEntity> : List<Expression<Func<TEntity, bool>>>
{
}