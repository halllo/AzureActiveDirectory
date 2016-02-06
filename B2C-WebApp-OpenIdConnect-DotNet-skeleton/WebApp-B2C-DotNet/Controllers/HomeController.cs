﻿using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using WebApp_OpenIDConnect_DotNet_B2C.Policies;

namespace WebApp_OpenIDConnect_DotNet_B2C.Controllers
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
			Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
			ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
			return View();
		}

		public ActionResult Error(string message)
		{
			ViewBag.Message = message;

			return View("Error");
		}
	}
}