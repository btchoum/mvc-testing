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
            var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.Setup(r => r.UserHostAddress).Returns("127.0.0.1");

            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s.SessionID).Returns(Guid.NewGuid().ToString());

            IPrincipal currentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            var context = new Mock<HttpContextBase>();
            context.SetupGet(c => c.Request).Returns(request.Object);
            context.SetupGet(c => c.Response).Returns(response.Object);
            context.SetupGet(c => c.Server).Returns(server.Object);
            context.SetupGet(c => c.User).Returns(currentUser);

            HttpContext.Current = MockHelpers.FakeHttpContext();
            HttpContext.Current.Session["SomeSessionVariable"] = 123;
            //context.SetupGet(c => c.Session).Returns(session.Object);
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);

            context.SetupGet(c => c.Session).Returns(sessionBase);

            // Arrange
            HomeController controller = new HomeController();
            controller.ControllerContext = new ControllerContext(context.Object,
                                                new RouteData(), controller);
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
