using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using OauthScratch.MvcOwinClient.OpenIdConnect;

namespace OauthScratch.MvcOwinClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Claims()
        {
            ViewBag.Message = "Claims";
            return View();
        }

        [Authorize]
        public ActionResult UserInfo()
        {
            ViewBag.Message = "User Info";
            var principal = User as ClaimsPrincipal;
            return View(principal.GetUserInfo());
        }

        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}