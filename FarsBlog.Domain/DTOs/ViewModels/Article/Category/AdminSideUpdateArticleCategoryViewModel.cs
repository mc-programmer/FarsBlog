using FarsBlog.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class AdminSideUpdateArticleCategoryViewModel
{
    public int? Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Title { get; set; }

    [Display(Name = "عنوان در URL")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Slug { get; set; }

    public string? CoverName { get; set; }

    [Display(Name = "کاور")]
    public IFormFile? CoverImage { get; set; }

}