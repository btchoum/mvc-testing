using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RequestTracker.Web.Controllers;
using RequestTracker.Web.Models;

namespace RequestTracker.Tests.Integration.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Index() as ViewResult;

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
            var result = controller.About() as ViewResult;
            var appUser = result.ViewData["ApplicationUser"] as ApplicationUser;

            // Assert
            Assert.That(appUser, Is.Not.Null);
            Assert.That(appUser.Username, Does.Contain(Environment.UserName).IgnoreCase);
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Contact() as ViewResult;
            var appUser = result.ViewData["ApplicationUser"] as ApplicationUser;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(appUser, Is.Not.Null);
            Assert.That(appUser.Username, Does.Contain(Environment.UserName).IgnoreCase);
        }
    }
}
