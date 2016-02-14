using System.Web.Mvc;
using TaskWebApp.Policies;

namespace TaskWebApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[PolicyAuthorize(Policy = "B2C_1_policy2")]
		public ActionResult Claims()
		{
			ViewBag.Message = "Your application description page.";
			return View();
		}

		public ActionResult Error(string message)
		{
			ViewBag.Message = message;

			return View("Error");
		}
	}
}