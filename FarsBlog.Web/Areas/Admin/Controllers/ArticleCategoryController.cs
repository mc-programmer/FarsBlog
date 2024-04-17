using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Shared;
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

    [HttpGet()]
    public async Task<IActionResult> List(FilterArticleCategoryViewModel filter)
    {
        filter.TakeEntity = 10;

        var result = await _articleCategoryService.FilterArticleCategoriesAsync(filter);

        return View(result.Value);
    }

    [HttpGet]
    public async Task<PartialViewResult> GetPartialList(FilterArticleCategoryViewModel filter)
    {
        filter.TakeEntity = 10;

        var result = await _articleCategoryService.FilterArticleCategoriesAsync(filter);

        return PartialView("_ArticleCategoryListPartial", result.Value);
    }

    #region Create

    [HttpGet]
    public ViewResult Create() => View();

    public async Task<IActionResult> Create(AdminSideUpsertArticleCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ToastrSuccessMessage] = ErrorMessages.NullValue;
            return View(model);
        }

        var result = await _articleCategoryService.CreateArticleCategoryAsync(model);

        if (result.IsFailure)
        {
            TempData[ErrorMessage] = result.Message;
            return View(model);
        }

        TempData[ToastrSuccessMessage] = result.Message;
        return RedirectToAction(actionName:nameof(List));
    }

    #endregion

    #region Update

    public async Task<IActionResult> Update(int id)
    {
        var result = await _articleCategoryService.GetArticleCategoryByIdForAdminUpdate(id);

        if (result.IsFailure)
        {
            TempData[ToastrErrorMessage] = result.Message;
            return RedirectToAction(nameof(List));
        }

        return View(result.Value);
    }

    [HttpPost,ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminSideUpsertArticleCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = ErrorMessages.NullValue;
            return View(model);
        }

        var result = await _articleCategoryService.UpdateArticleCategoryAsync(model);

        if (result.IsFailure)
        {
            TempData[ToastrErrorMessage] = result.Message;
            return View(model);
        }

        TempData[ToastrSuccessMessage] = result.Message;
        return RedirectToAction(nameof(List));
    }

    #endregion

    #endregion
}
