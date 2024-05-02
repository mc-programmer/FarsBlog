using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Areas.Admin.Controllers
{
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

        //[HttpPost]
        //public async Task<IActionResult> Create(AdminSideArticleDetailsForFilterViewModel)
        //{
            
        //}

        #endregion

        #endregion
    }
}