using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NBlog.Application.UserApp;
using NBlog.Web.Areas.Manage.Models;
using NBlog.Utility;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NBlog.Web.Areas.Manage.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : BaseController
    {
        private IUserAppService _userservice;

        public LoginController(IUserAppService userAppService)
        {
            _userservice = userAppService;
        }

        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /<controller>/Index
        /// <summary>
        /// 登录post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //检查用户信息
                var user = _userservice.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //记录Session
                    HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                    //跳转到管理系统首页
                    return RedirectToAction("index", "home");
                }
                ViewBag.ErrorInfo = "用户名或密码错误。";
                return View();
            }
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    ViewBag.ErrorInfo = item.Errors[0].ErrorMessage;
                    break;
                }
            }
            return View(model);
        }

        
    }
}
