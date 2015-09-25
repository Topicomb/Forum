using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Topicomb.Forum.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var ret = DB.Forums
                .Include(x => x.SubForums)
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.PRI)
                .ToList();
            
            // populating
            foreach(var forum in ret)
            {
                foreach(var subforum in forum.SubForums)
                {
                    subforum.SubForums = DB.Forums
                        .Where(x => x.ParentId != null && x.ParentId.Value == subforum.Id)
                        .ToList();
                }
            }
            
            return View(ret);
        }
    }
}
