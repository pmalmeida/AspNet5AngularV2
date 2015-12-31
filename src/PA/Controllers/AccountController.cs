using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;

namespace PA.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        #region Configuration
        private IConfigurationRoot _Configuration;
        public IConfigurationRoot Configuration
        {
            get
            {
                if (_Configuration == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables();
                    _Configuration = builder.Build();
                }
                return _Configuration;
            }
        }
        #endregion

        [HttpPost]
        [Route("api/login")]
        public IActionResult Post([FromBody]Credentials credentials)
        {
            var adminUserName = Configuration["ApplicationSettings:AdminUserName"];
            var adminPassword = Configuration["ApplicationSettings:AdminPassword"];

            if (adminUserName != credentials.Username || adminPassword != credentials.Password)
            {
                //HttpContext.Session["IsLogged"] = "false";
                return HttpUnauthorized();
            }
            else {
                //HttpContext.Session["IsLogged"] = "true";
                return Ok();
            }
        }


        public class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
