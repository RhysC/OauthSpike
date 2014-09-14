using System.Web;
using System.Web.Mvc;

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

        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}