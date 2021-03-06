using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.TestHost;
using Topicomb.Forum.Models;
using Newtonsoft.Json;
using Xunit;

namespace Topicomb.Forum.FunctionalTests
{
    public class AccountTests : HostTestFixture
    {
        [Fact]
        public async Task login_failed_test()
        {
            // Act 1
            var result = await client.GetAsync("/Account/Login");

            // Arrange
            var cookieToken = RetrieveAntiforgeryCookie(result);
            var csrf = RetrieveAntiforgeryToken(await result.Content.ReadAsStringAsync(), "Account/Login");

            // Act 2
            var request = new HttpRequestMessage(HttpMethod.Post, "/Account/Login");
            request.Headers.Add("Cookie", cookieToken.Key + "=" + cookieToken.Value);
            request.Content = PostUrl(new { Username = "admin", Password = "000000", __RequestVerificationToken = csrf, RememberMe = "false" });
            result = await client.SendAsync(request);
            
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Forbidden, result.StatusCode);
        }
        
        [Fact]
        public async Task login_succeeded_test()
        {
            // Act 1
            var result = await client.GetAsync("/Account/Login");

            // Arrange
            var cookieToken = RetrieveAntiforgeryCookie(result);
            var csrf = RetrieveAntiforgeryToken(await result.Content.ReadAsStringAsync(), "Account/Login");

            // Act 2
            var request = new HttpRequestMessage(HttpMethod.Post, "/Account/Login");
            request.Headers.Add("Cookie", cookieToken.Key + "=" + cookieToken.Value);
            request.Content = PostUrl(new { Username = "admin", Password = "123456", __RequestVerificationToken = csrf, RememberMe = "false" });
            result = await client.SendAsync(request);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Redirect, result.StatusCode);
            Assert.Equal("/Account/LoginSuccess", result.Headers.Location.OriginalString);
        }
        
        [Fact(Skip = "The frontend works are not finished.")]
        public async Task logout_after_login_test()
        {
            // Act 1
            var result = await client.GetAsync("/Account/Login");

            // Arrange
            var cookieToken = RetrieveAntiforgeryCookie(result);
            var csrf = RetrieveAntiforgeryToken(await result.Content.ReadAsStringAsync(), "Account/Login");

            // Act 2
            var request = new HttpRequestMessage(HttpMethod.Post, "/Account/Login");
            request.Headers.Add("Cookie", cookieToken.Key + "=" + cookieToken.Value);
            request.Content = PostUrl(new { Username = "admin", Password = "123456", __RequestVerificationToken = csrf, RememberMe = "false" });
            result = await client.SendAsync(request);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Redirect, result.StatusCode);
            Assert.Equal("/Account/LoginSuccess", result.Headers.Location.OriginalString);
            
            // Act 3
            request = new HttpRequestMessage(HttpMethod.Post, "/");
            request.Headers.Add("Cookie", cookieToken.Key + "=" + cookieToken.Value);
            request.Content = PostUrl(new { __RequestVerificationToken = csrf });
            result = await client.SendAsync(request);
            
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Redirect, result.StatusCode);
        }
    }
}
