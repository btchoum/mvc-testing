using RequestTracker.Web.Infrastructure;
using RequestTracker.Web.Models;
using System.Web.Http;

namespace RequestTracker.Web.Controllers
{
    public class PersonController : ApiController
    {
        private readonly UserService _userService;

        public PersonController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        [Route("api/people")]
        public IHttpActionResult Get()
        {
            var person = _userService.GetLoggedInUser();

            return Ok(person);
        }
    }
}
