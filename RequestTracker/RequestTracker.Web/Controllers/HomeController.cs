using RequestTracker.Web.Infrastructure;
using System.Web.Mvc;

namespace RequestTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

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
            var appUser = DbClass.ApplicationUser;

            ViewData["ApplicationUser"] = appUser;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var appUser = _userService.GetLoggedInUser();

            ViewData["ApplicationUser"] = appUser;


            return View();
        }
    }
}