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
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Identity;
using Topicomb.Forum.Controllers;
using Topicomb.Forum.Models;
using CodeComb.Testing;
using Xunit;
using Moq;

namespace Topicomb.Forum.Tests
{
    public class AccountControllerTests : MvcTestFixture<Startup>
    {
        [Fact]
        public async Task login_succeeded_test()
        {
            // Arrange
            var services = GenerateServiceProvider();
            await SampleData.InitDB(services);
            var db = services.GetRequiredService<ForumContext>();
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;

            // Act
            var controller = new AccountController
            {
                ActionContext = new ActionContext(httpContext, new Microsoft.AspNet.Routing.RouteData(), new ActionDescriptor())
            };
            controller.Prepare();
            var result = await controller.Login("admin", "123456", false) as RedirectToActionResult;

            // Assert
            Assert.Equal("Account", result.ControllerName);
            Assert.Equal("LoginSuccess", result.ActionName);
        }

        [Fact]
        public async Task login_failed_test()
        {
            // Arrange
            var services = GenerateServiceProvider();
            await SampleData.InitDB(services);
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

        [Fact]
        public async Task signed_in_test()
        {
            // Arrange
            var services = GenerateServiceProvider(mockHttpContext: x =>
            {
                x.Setup(y => y.User)
                    .Returns(CreateAppIdentity());
            });
            await SampleData.InitDB(services);
            var db = services.GetRequiredService<ForumContext>();
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;

            // Act
            var controller = new AccountController
            {
                ActionContext = new ActionContext(httpContext, new Microsoft.AspNet.Routing.RouteData(), new ActionDescriptor())
            };
            controller.Prepare();

            // Assert
            Assert.True(controller.User.IsSignedIn());
            Assert.Equal("admin", controller.User.GetUserName());
            Assert.Equal("1", controller.User.GetUserId());
            Assert.NotNull(controller.CurrentUser);
            Assert.Equal("admin", controller.CurrentUser.UserName);
            Assert.Equal(1, controller.CurrentUser.Id);
        }
    }
}
