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
            List<MenuExt> menuList = new List<MenuExt>();
            if (currentUser.FullName == "项目组组长" || currentUser.FullName == "技术主管" || currentUser.FullName == "项目主管" || currentUser.FullName == "人事主管")
            {
                menuList = MenuDal.Instance.GetMenusByRoleName("学生");
            }
            else if (currentUser.FullName == "人事经理" || currentUser.FullName == "项目经理")
            {
                menuList = MenuDal.Instance.GetMenusByRoleName("人事经理");
            }
            else
            {
                menuList = MenuDal.Instance.GetMenusByRoleName(currentUser.FullName);
            }
            return new DeluxeJsonResult(new { data = menuList, code = 200 }, "yyyy-MM-dd HH:mm");
        }
    }
}