using HPIT.Evalute.Data.Model;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.ExtEntitys;
using HPIT.Survey.Portal.Common;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.User = DeluxeUser.CurrentMember;
            return View();
        }

        public DeluxeJsonResult GetCurrentRoleMenus()
        {
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            List<MenuExt> menuList = MenuDal.Instance.GetMenusByRoleName(currentUser.FullName);
            return new DeluxeJsonResult(new { data = menuList, code = 200 }, "yyyy-MM-dd HH:mm");
        }
    }
}