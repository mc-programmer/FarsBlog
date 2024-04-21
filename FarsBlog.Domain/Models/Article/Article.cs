using FarsBlog.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.Models.Article;

public class Article : BaseEntity
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(200)]
    public string? Slug { get; set; }
}
