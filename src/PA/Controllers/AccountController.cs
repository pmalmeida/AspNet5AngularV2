using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using PA.Models;
using Microsoft.Extensions.Logging;

namespace PA.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IConfigurationRoot _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(IConfigurationRoot appSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Post([FromBody]Credentials credentials)
        {
            var adminUserName = _appSettings["ApplicationSettings:AdminUserName"];
            var adminPassword = _appSettings["ApplicationSettings:AdminPassword"];


            if (ModelState.IsValid)
            {

                //if (adminUserName != credentials.Username || adminPassword != credentials.Password)
                //{
                    var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation(1, "User logged in.");
                        return Ok(result);
                        //return RedirectToLocal(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return Ok(result);
                        //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(2, "User account locked out.");
                        return HttpUnauthorized();
                        //return View("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return HttpUnauthorized();
                        //return View(model);
                    }

                    //return HttpUnauthorized();
                //}
                //else {
                //    //HttpContext.Session["IsLogged"] = "true";
                //    return Ok();
                //}
            }

            return HttpUnauthorized();
        }


        public class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
