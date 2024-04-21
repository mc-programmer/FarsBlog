using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.Models.Common;

public abstract class BaseEntity<T> where T : IEquatable<T>
{
#pragma warning disable CS8618
    [Key]
    public T Id { get; set; }
#pragma warning restore CS8618

    public DateTime CreateDateOnUtc { get; set; } = DateTime.UtcNow;

    public bool IsDelete { get; set; }
}
public abstract class BaseEntity : BaseEntity<int> { }