﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Topicomb.Forum.Models;
using Xunit;

namespace Topicomb.Forum.FunctionalTests
{
    public class MiddlewaresTests : HostTestFixture
    {
        [Fact]
        public async Task localization_js_test()
        {
            var result = await client.GetStringAsync("/assets/shared/scripts/localization.js");
            Assert.NotEmpty(result);
            Assert.True(result.IndexOf("__dictionary =") >= 0);
            Assert.True(result.IndexOf("__replaceAll") >= 0);
        }
    }
}
