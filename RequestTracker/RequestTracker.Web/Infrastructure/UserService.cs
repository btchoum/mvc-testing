using RequestTracker.Web.Models;

namespace RequestTracker.Web.Infrastructure
{
    public class UserService
    {
        public ApplicationUser GetLoggedInUser()
        {
            return DbClass.ApplicationUser;
        }
    }
}