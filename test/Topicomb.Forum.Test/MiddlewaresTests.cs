using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Topicomb.Forum;
using Topicomb.Forum.Models;
using Xunit;

namespace Topicomb.Forum.Test
{
    public class MiddlewaresTests
    {
        [Fact]
        public async Task Localization_JS_Test()
        {
            var server = new TestServer(TestServer.CreateBuilder().UseStartup<Startup>());
            var result = await server.CreateClient().GetStringAsync("/shared/localization.js");
            Assert.NotEmpty(result);
        }
        
        /*
        [Fact]
        public async Task Test()
        {
            var server = new TestServer(TestServer.CreateBuilder().UseStartup<Startup>());
            var client = server.CreateRequest("/");
            var result = await client.GetAsync();
            Assert.Equal("ok", await result.Content.ReadAsStringAsync());
        }
        */
    }
}
