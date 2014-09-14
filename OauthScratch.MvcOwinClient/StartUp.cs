using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using OauthScratch.MvcOwinClient;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OauthScratch.MvcOwinClient
{
    public static class Constants
    {
        public const string BaseAddress = "http://localhost:3344/core";//The server project URL
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SetUpAuth(app);
        }

        private static void SetUpAuth(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "implicitclient",
                Authority = Constants.BaseAddress,
                RedirectUri = "http://localhost:51207/",
                ResponseType = "id_token token",
                Scope = "openid email",
                SignInAsAuthenticationType = "Cookies",

                // sample how to access token on form (for token response type)
                //Notifications = new OpenIdConnectAuthenticationNotifications
                //{
                //    MessageReceived = async n =>
                //        {
                //            var token = n.ProtocolMessage.Token;

                //            if (!string.IsNullOrEmpty(token))
                //            {
                //                n.OwinContext.Set<string>("idsrv:token", token);
                //            }
                //        },
                //    SecurityTokenValidated = async n =>
                //        {
                //            var token = n.OwinContext.Get<string>("idsrv:token");

                //            if (!string.IsNullOrEmpty(token))
                //            {
                //                n.AuthenticationTicket.Identity.AddClaim(
                //                    new Claim("access_token", token));
                //            }
                //        }
                //}
            });
        }
    }
}