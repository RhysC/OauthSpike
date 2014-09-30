using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using OauthScratch.MvcOwinClient;
using OauthScratch.MvcOwinClient.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OauthScratch.MvcOwinClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            app.SetUpAuth();
        }
    }
}