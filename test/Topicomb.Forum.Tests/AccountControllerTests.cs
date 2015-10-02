using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Topicomb.Forum.Controllers;
using Topicomb.Forum.Models;
using CodeComb.Testing;
using Xunit;

namespace Topicomb.Forum.Tests
{
    public class AccountControllerTests : MvcTestFixture<Startup>
    {
        [Fact]
        public async Task login_succeeded_test()
        {
            // Arrange
            var services = GenerateServiceProvider();
            var db = services.GetRequiredService<ForumContext>();
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;

            // Act
            var controller = new AccountController
            {
                ActionContext = new ActionContext(httpContext, new Microsoft.AspNet.Routing.RouteData(), new ActionDescriptor())
            };
            controller.Prepare();
            await controller.Login("admin", "123456", false);
           
            // Assert
            Assert.True(controller.User.IsSignedIn());
        }

        [Fact]
        public async Task login_failed_test()
        {
            // Arrange
            var services = GenerateServiceProvider();
            var db = services.GetRequiredService<ForumContext>();
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;

            // Act
            var controller = new AccountController
            {
                ActionContext = new ActionContext(httpContext, new Microsoft.AspNet.Routing.RouteData(), new ActionDescriptor())
            };
            controller.Prepare();
            await controller.Login("admin", "000000", false);

            // Assert
            Assert.False(controller.User.IsSignedIn());
        }
    }
}
