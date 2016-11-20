using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NBlog.Web.Areas.Manage.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NBlog.Web.Areas.Manage.Controllers
{
    [ServiceFilter(typeof(LoginActionFilter))]//登录拦截 必须要登录
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("index", "menu");
        }
    }
}
