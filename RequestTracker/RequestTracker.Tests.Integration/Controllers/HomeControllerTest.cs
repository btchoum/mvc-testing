using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using RequestTracker.Web.Controllers;

namespace RequestTracker.Tests.Integration.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            var controller = CreateController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            var loggedInUser = result.ViewData["UserName"] as string;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(loggedInUser, Is.Not.Null);
                Assert.That(loggedInUser, Does.Contain(Environment.UserName).IgnoreCase);
                Assert.That(loggedInUser, Does.Contain(Environment.UserDomainName).IgnoreCase);
            });
        }

        private HomeController CreateController()
        {
            HttpContext.Current = MockHelpers.FakeHttpContext();
            var context = new HttpContextWrapper(HttpContext.Current);

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            return controller;
        }

        [Test]
        public void About()
        {
            // Arrange
            var controller = CreateController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            var controller = CreateController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
