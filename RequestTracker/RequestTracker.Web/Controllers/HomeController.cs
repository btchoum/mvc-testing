using System.Web;
using System.Web.Mvc;

namespace RequestTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sessionId = HttpContext.Session.SessionID.ToUpper();
            var username = User.Identity.Name.ToLower();

            var fromDbClass = DbClass.UserName;

            Session["loggedInUser"] = username;

            ViewData["UserName"] = Session["loggedInUser"].ToString();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    internal class DbClass
    {
        public static object UserName
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
    }
}