using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace AadSamplePortal.Misc
{
public class ChallengeResult : HttpUnauthorizedResult
{
    public ChallengeResult(string redirectUrl)
    {
        RedirectUrl = redirectUrl;
    }

    public string RedirectUrl { get; private set; }

    public override void ExecuteResult(ControllerContext context)
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = RedirectUrl
        };

        context.HttpContext.GetOwinContext().Authentication.Challenge(
            authenticationProperties,
            OpenIdConnectAuthenticationDefaults.AuthenticationType);
    }
}
}