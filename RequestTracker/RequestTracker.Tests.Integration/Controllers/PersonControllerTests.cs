using NUnit.Framework;
using RequestTracker.Web.Controllers;
using RequestTracker.Web.Models;
using System;
using System.Web;
using System.Web.Http.Results;

namespace RequestTracker.Tests.Integration.Controllers
{
    [TestFixture]
    public class PersonControllerTests
    {
        [Test]
        public void PersonController_Get()
        {
            var controller = CreateController();

            var result = controller.Get() as OkNegotiatedContentResult<ApplicationUser>;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Content, Is.Not.Null);
                Assert.That(result.Content.Username, Does.Contain(Environment.UserName).IgnoreCase);
            });
        }

        private static PersonController CreateController()
        {
            HttpContext.Current = MockHelpers.FakeHttpContext();

            var controller = new PersonController();

            return controller;
        }
    }
}
