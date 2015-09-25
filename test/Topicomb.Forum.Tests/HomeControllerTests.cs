using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc.ActionResults;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Identity;
using CodeComb.Testing;
using Topicomb.Forum.Controllers;
using Topicomb.Forum.Models;
using Xunit;
using Moq;

namespace Topicomb.Forum.Tests
{
    public class HomeControllerTests : TestContext<Startup>
    {
        [Fact]
        public void index_action()
        {
            // Arrange
            var services = GenerateServiceProvider();
            var db = services.GetRequiredService<ForumContext> ();
            var httpContext = services.GetRequiredService<IHttpContextAccessor> ().HttpContext;
            var theory = db.Forums
                .Include(x => x.SubForums)
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.PRI)
                .ToList();
            
            // Act
            var controller = new HomeController
            {
                ActionContext = new Microsoft.AspNet.Mvc.ActionContext(httpContext, new Microsoft.AspNet.Routing.RouteData(), new Microsoft.AspNet.Mvc.Actions.ActionDescriptor())
            };
            controller.Prepare();
            var result = controller.Index() as ViewResult;
            
            // Assert
            Assert.Equal(theory, result.ViewData.Model);
        }
    }
}
