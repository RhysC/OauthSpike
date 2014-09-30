using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace OauthScratch.MvcOwinClient.OpenIdConnect
{
    /// <summary>
    /// Custom implementation that follow the OpenIdConnect Guidelines
    /// </summary>
    public static class OpenIdConnectExtensions
    {
        public static void SetUpAuth(this IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Constants.SignInAsAuthenticationType
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "implicitclient",
                Authority = Constants.BaseAddress,
                RedirectUri = "http://localhost:51207/",
                ResponseType = "id_token token",
                Scope = "openid profile email",
                SignInAsAuthenticationType = Constants.SignInAsAuthenticationType,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = (context) =>
                        Task.Run(() =>
                        {
                            var token = context.OwinContext.Get<string>("idsrv:token");
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.AuthenticationTicket.Identity.AddClaim(new Claim(Constants.AccessTokenClaimKey, token));
                            }
                        }),
                    MessageReceived = (context) =>
                        Task.Run(() =>
                        {
                            var token = context.ProtocolMessage.Token;
                            if (!string.IsNullOrEmpty(token))//should be included if the  ResponseType == "id_token token" (specifically the second token term) 
                            {
                                context.OwinContext.Set("idsrv:token", token);
                            }
                        }),
                }
            });
        }

        public static Dictionary<string, string> GetUserInfo(this ClaimsPrincipal principal)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", principal.FindFirst(Constants.AccessTokenClaimKey).Value);

            var result = client.GetStringAsync(Constants.BaseAddress + "/connect/userinfo").Result;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        }
    }
}