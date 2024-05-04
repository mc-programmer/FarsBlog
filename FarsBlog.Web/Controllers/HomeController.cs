using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
