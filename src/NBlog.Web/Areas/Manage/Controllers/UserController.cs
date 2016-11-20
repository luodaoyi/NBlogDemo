using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NBlog.Web.Areas.Manage.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NBlog.Web.Areas.Manage.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ServiceFilter(typeof(LoginActionFilter))]
    public class UserController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        } 


    }
}
