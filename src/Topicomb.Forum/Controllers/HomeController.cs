using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Topicomb.Forum.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return Content(DB.Users.Count().ToString());
        }
    }
}
