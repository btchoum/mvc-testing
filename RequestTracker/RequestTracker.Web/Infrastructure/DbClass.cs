using RequestTracker.Web.Models;
using System.Web;
using System.Web.SessionState;

namespace RequestTracker.Web.Infrastructure
{
    public class DbClass
    {
        public static string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public static ApplicationUser ApplicationUser
        {
            get
            {
                if (Session["AppUser"] is ApplicationUser user) return user;

                Session["AppUser"] = GetUserFromDatabase(UserName);

                return Session["AppUser"] as ApplicationUser;
            }
        }

        private static HttpSessionState Session => HttpContext.Current.Session;

        private static ApplicationUser GetUserFromDatabase(string userName)
        {
            return new ApplicationUser() { Username = userName };
        }
    }
}