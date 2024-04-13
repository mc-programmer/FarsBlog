using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Areas.Admin.Controllers
{
    public class ArticleCategoryController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
