using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Topicomb.Forum.Models;
using CodeComb.HtmlAgilityPack;
using Newtonsoft.Json;
using Xunit;

namespace Topicomb.Forum.Test
{
    public class AccountControllerTests : TestHost
    {
        [Fact]
        public async Task Login_Succeeded_Test()
        {
            // Act 1
            var result = await client.GetAsync("/Account/Login");

            // Arrange
			var html = GetHtml(await result.Content.ReadAsStringAsync());
            var csrf = html.DocumentNode.GetRequestVerificationToken("frmLogin");
            
            // Act 2
            result = await client.PostAsync("/Account/Login", PostData(new { Username = "admin", Password = "123456", __RequestVerificationToken = csrf }));

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.Redirect, result.StatusCode);
            Assert.Equal("/", await result.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async Task Login_Failed_Test()
        {
            // Act 1
            var result = await client.GetAsync("/Account/Login");

            // Arrange
			var html = GetHtml(await result.Content.ReadAsStringAsync());
            var csrf = html.DocumentNode.GetRequestVerificationToken("frmLogin");

            // Act 2
            result = await client.PostAsync("/Account/Login", PostData(new { Username = "admin", Password = "000000", __RequestVerificationToken = csrf }));

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.Redirect, result.StatusCode);
            Assert.Equal("/", await result.Content.ReadAsStringAsync());
        }
    }
}
