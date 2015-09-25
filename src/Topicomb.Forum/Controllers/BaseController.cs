using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Topicomb.Forum.Models;

namespace Topicomb.Forum.Controllers
{
    public class BaseController : BaseController<User, ForumContext, long>
    {
        public override void Prepare()
        {
            base.Prepare();
            ViewBag.Config = Startup.Configuration;
        }
    }
}
