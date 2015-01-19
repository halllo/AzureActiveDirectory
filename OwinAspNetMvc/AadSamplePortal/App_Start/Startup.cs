using System;
using AadSamplePortal;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace AadSamplePortal
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "Application",
				LoginPath = new PathString("/Account/LogOn"),
				ReturnUrlParameter = "returnUrl"
			});

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "External",
				AuthenticationMode = AuthenticationMode.Passive,
				ExpireTimeSpan = TimeSpan.FromMinutes(5.0)
			});

			app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			{
				AuthenticationMode = AuthenticationMode.Passive,

				// specify the client ID of the application provisioned within your Azure AD tenant
				// (see 'Configure' tab of the application entry)
				ClientId = "",

				// specify your Azure AD tenant as identity provider:
				// https://login.windows.net/ followed by the domain name of the Azure AD tenant
				Authority = "https://login.windows.net/manuelnaujoks0aadsample.onmicrosoft.com",

				SignInAsAuthenticationType = "External",
				PostLogoutRedirectUri = "https://localhost:44310/Account/LoggedOff"
			});
		}
	}
}