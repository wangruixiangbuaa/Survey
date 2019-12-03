using HPIT.Evalute.Data;
using HPIT.Evalute.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace HPIT.Survey.Portal.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 允许匿名登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        // GET: Login
        public JsonResult LoginIn(string username, string password)
        {
            List<HPITMemberInfo> users = EvaluteDal.Instance.LoginMember(username,password);
            JsonResult jsonResult = new JsonResult();
            //如果找到的话，就添加到缓存当中，并且跳转到主页面
            if (users != null && users.Count >=1)
            {
                string json = JsonConvert.SerializeObject(users.FirstOrDefault());
                HttpCookie cokie = new HttpCookie("login",Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json)));
                Response.Cookies.Add(cokie);
                jsonResult.Data = new { data = json, state = "200" };
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return jsonResult;
            }
            else
            {
                //RedirectToAction("Index", "Login", false);
                jsonResult.Data = new { data = "未找到用户!", state = "403" };
                return jsonResult;
            }
        }

        public ActionResult LogOff()
        {
            Request.Cookies.Clear();
            Response.Cookies.Clear();
            return View("Index");
        }
    }
}