using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Areas.Admin.Controllers;

public class ArticleCategoryController : AdminBaseController
{
    #region Fields

    private readonly IArticleCategoryService _articleCategoryService;

    #endregion

    #region Constructor

    public ArticleCategoryController(IArticleCategoryService articleCategoryService)
    {
        _articleCategoryService = articleCategoryService;
    }

    #endregion

    #region Actions

    public async Task<IActionResult> List(FilterArticleCategoryViewModel filter)
    {
        filter.TakeEntity = 10;

        var result = await _articleCategoryService.FilterArticleCategoriesAsync(filter);

        return View(result);
    }

    #endregion
}
