﻿using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace RequestTracker.Tests.Integration.Controllers
{
    public static class MockHelpers
    {
        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var httpResponce = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(httpRequest, httpResponce);

            httpContext.User = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            var sessionId = Guid.NewGuid().ToString();
            var sessionContainer = new HttpSessionStateContainer(sessionId, new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }
    }
}
