using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Testing;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Compilation;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.AspNet.Hosting.Builder;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Hosting.Startup;
using CodeComb.Testing;
using Xunit;
using Moq;
using Topicomb.Forum.Models;

namespace Topicomb.Forum.Tests
{
    public class InitDBTests : TestContext<Startup>
    {
        [Fact]
        public async Task database_init_test()
        {
            // Arrange
            var services = GenerateServiceProvider();
            var db = services.GetRequiredService<ForumContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();

            // Act
            await SampleData.InitDB(services);
            

            // Assert
            Assert.NotNull(db.Database);
            Assert.True(await roleManager.RoleExistsAsync("Root"));
            Assert.True(await roleManager.RoleExistsAsync("Super Moderator"));
            Assert.True(await roleManager.RoleExistsAsync("Moderator"));
            Assert.True(await roleManager.RoleExistsAsync("Member"));
            Assert.True(await roleManager.RoleExistsAsync("Blocked"));
            Assert.Equal(1, db.Users.Count());
            Assert.Equal("admin", db.Users.First().UserName);
        }
    }
}
