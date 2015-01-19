using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AadSamplePortal.Misc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace AadSamplePortal.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            var callbackUrl = Url.Action("LogOnCallback", new {returnUrl});

            // workaround for issue with OWIN implementation: https://katanaproject.codeplex.com/workitem/197
            // if no session cookie is set, External Cookie won't get set
            // description see: http://stackoverflow.com/questions/20737578/asp-net-sessionid-owin-cookies-do-not-send-to-browser
            Session["OwinWorkaround"] = true;

            return new ChallengeResult(callbackUrl);
        }

        [AllowAnonymous]
        public async Task<ActionResult> LogOnCallback(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            var externalAuthenticateResult =
                await AuthenticationManager.AuthenticateAsync("External");
            if (externalAuthenticateResult == null)
            {
                throw new InvalidOperationException("No external identity present.");
            }

            var externalIdentity = externalAuthenticateResult.Identity;

            var identity = new ClaimsIdentity(externalIdentity.Claims, "Application");

            // add identity issuer as identity provider claim (required for anti-forgery token)
            var identityIssuer = identity.FindFirst(ClaimTypes.NameIdentifier).Issuer;
            identity.AddClaim(
                new Claim(
                    "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    identityIssuer));

            // sign out external identity
            AuthenticationManager.SignOut("External");

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            // sign in identity
            AuthenticationManager.SignIn(authenticationProperties, identity);

            return Redirect(returnUrl);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(
                "Application",
                OpenIdConnectAuthenticationDefaults.AuthenticationType);

            return RedirectToAction("LoggedOff");
        }

        [AllowAnonymous]
        public ActionResult LoggedOff()
        {
            return View();
        }
    }
}