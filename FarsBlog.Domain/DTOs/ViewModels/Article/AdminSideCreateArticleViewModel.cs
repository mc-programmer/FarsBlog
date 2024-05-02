using FarsBlog.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace FarsBlog.Domain.DTOs.ViewModels.Article;

public class AdminSideCreateArticleViewModel
{
#pragma warning disable CS8618

    [Display(Name ="عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(250,ErrorMessage =ErrorMessages.MaxLengthError)]
    public string Title { get; set; }

    [Display(Name = "عنوان در Url")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression("^[^<#,*,%,(,),+,@,\\,/, ,!>]+$", ErrorMessage = "متن وارد شده نباید شامل کاراکترهای (#,*,%,(,),+,@,\\,/,!) باشد")]
    public string Slug { get; set; }
   
    [Display(Name = "توضیحات کوتاه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ShortDescription { get; set; }

    [Display(Name = "تصویر مقالات")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name ="عنوان تصویر مقاله")]
    public  string? ImageAlt  { get; set; }

    public string ImageFileName { get; set; }

    [Display(Name = "متن")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Text { get; set; }

    public bool IsPublished { get; set; }
}
