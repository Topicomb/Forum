using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.DependencyInjection;
using Topicomb.Forum.Controllers;
using Topicomb.Forum.Models;
using Xunit;

namespace Topicomb.Forum.Test
{
    public class HomeControllerTest : VirtualEnvironment
    {
        [Fact]
        public void DatabaseContext()
        {
            // Arrange
            var controller = new HomeController() 
            {
               DB = services.GetRequiredService<ForumContext> ()
            };

            // Assert
            Assert.NotNull(controller.DB);
        }
    }
}
