using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article;
using FarsBlog.Domain.Shared;
using FarsBlog.Web.Results;
using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Areas.Admin.Controllers;

public class ArticleController : AdminBaseController
{
    #region Fields

    private readonly IArticleService _articleService;

    #endregion

    #region Constructor

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    #endregion

    #region Actions

    #region List

    public async Task<IActionResult> Index(FilterArticleViewModel filter)
    {
        filter.TakeEntity = 10;
        var result = await _articleService.AdminFilterAsync(filter);

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPartialList(FilterArticleViewModel filter)
    {
        filter.TakeEntity = 10;
        var result = await _articleService.AdminFilterAsync(filter);

        return PartialView("_ArticleListPartial", result);
    }

    #endregion

    #region Create

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(AdminSideCreateArticleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ToastrErrorMessage] = ErrorMessages.NullValue;
            return View(model);
        }

        var result = await _articleService.CreateAsync(model);

        if (result.IsFailure)
        {
            TempData[ToastrErrorMessage] = result.Message;
            return View(model);
        }

        TempData[ToastrSuccessMessage] = result.Message;
        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Update

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _articleService.GetArticleByIdForEditByAdminAsync(id);

        if (result.IsFailure)
        {
            TempData[ToastrErrorMessage] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminSideUpdateArticleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData[ToastrErrorMessage] = ErrorMessages.NullValue;
           return View(model);
        }

        var result = await _articleService.UpdateAsync(model);

        if(result.IsFailure)
        {
            TempData[ToastrErrorMessage] = result.Message;
            return View(model);
        }

        TempData[ToastrSuccessMessage] = result.Message;
        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Delete

    [HttpPost]
    public async Task<JsonResult> Delete(int id)
    {
        var result = await _articleService.DeleteAsync(id);

        if(result.Value is false)
            return JsonResponse.Failure(ErrorMessages.NotFoundErorr);

        return JsonResponse.Success();
    }


    #endregion


    #endregion
}