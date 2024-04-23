using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Shared;
using FarsBlog.Web.Results;
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

    #region List

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

    #endregion

    #region Create

    [HttpGet]
    public PartialViewResult Create() => PartialView("_CreateCategoryPartial");

    [HttpPost]
    public async Task<IActionResult> Create(AdminSideCreateArticleCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return new JsonResponse(ErrorMessages.NullValue);

        var result = await _articleCategoryService.CreateArticleCategoryAsync(model);

        if (result.IsFailure)
            return new JsonResponse(result.Message);

        return new JsonResponse(result.Message, isSuccess: true);
    }

    #endregion

    #region Update

    public async Task<IActionResult> Update(int id)
    {
        var result = await _articleCategoryService.GetArticleCategoryByIdForAdminUpdate(id);

        if (result.IsFailure)
            return PartialView("_NotFoundModalPartial");

        return PartialView("_UpdateCategoryPartial", result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminSideCreateArticleCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return new JsonResponse(ErrorMessages.NullValue);

        var result = await _articleCategoryService.UpdateArticleCategoryAsync(model);

        if (result.IsFailure)
            return new JsonResponse(result.Message);

        return new JsonResponse(result.Message, isSuccess: true);
    }

    #endregion

    #region Delete

    [HttpPost]
    public async Task<JsonResult> Delete(int id)
    {
        var result = await _articleCategoryService.DeleteArticleCategoryAsync(id);

        if (result.IsFailure) 
            return new JsonResponse(result.Message);

        return new JsonResponse(result.Message,isSuccess:true);
    }

    #endregion

    #endregion
}