using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApp_OpenIDConnect_DotNet_B2C.Startup))]

namespace WebApp_OpenIDConnect_DotNet_B2C
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//https://azure.microsoft.com/de-de/documentation/articles/active-directory-b2c-devquickstarts-web-dotnet/


			ConfigureAuth(app);
		}
	}
}
