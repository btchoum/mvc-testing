using System.Web;

namespace RequestTracker.Web.Controllers
{
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