using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace Topicomb.Forum.Controllers
{
    public class AccountController : BaseController
    {
        
        [HttpGet]
        [GuestOnly]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [GuestOnly]
        public async Task<IActionResult> Login(string Username, string Password, bool RememberMe)
        {
            var result = await SignInManager.PasswordSignInAsync(Username, Password, RememberMe, false);
            if (result.Succeeded)
                return RedirectToAction("LoginSuccess", "Account", new { ReturnUrl = Request.Headers["Referer"].FirstOrDefault() });
            else
                return Error(Code: 403, Title: LocalizationManager.c("Login Failed"), Message: LocalizationManager.c("Checking your user name or password and retry."));
        }
        
        [HttpGet]
        public IActionResult LoginSuccess(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl))
                ReturnUrl = Url.Action("Index", "Home", null);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return Redirect(Request.Headers["Referer"].FirstOrDefault() ?? Url.Action("Index", "Home", null));
        }
    }
}
