using HPIT.Survey.Portal.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Newtonsoft.Json;
using HPIT.Evalute.Data.Model;
using System;
using HPIT.Data.Core;
using System.Web.Security;

namespace MVCLearn.Filters
{
    public class CustomAuthenticationFilter : IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //如果action 和 controller的添加的特性里面包含匿名的，则直接过滤掉。
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                return;
            }
            //获取请求上下问的context
            var Url = new UrlHelper(filterContext.RequestContext);
            //获取登录的url
            var urlstr = Url.Action("Index", "Login");
            //filterContext.Result = new RedirectResult(urlstr); //指定返回重定向登录界面
            //HttpCookie cokie = filterContext.HttpContext.Request.Cookies.Get("Login");
            //获取cookie 的名字
            string cookieName = FormsAuthentication.FormsCookieName;
            //当前context 里面取cookie
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[cookieName];
            //cookie为空 返回登录页面
            if (authCookie == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                       {
                                           {"controller","Login"},
                                           {"action","Index"},
                                           {"returnUrl",filterContext.HttpContext.Request.RawUrl }
                                       });
            }
            else
            {
                //定义票据
                FormsAuthenticationTicket authTicket = null;
                try
                {
                    //解密cookie的票据信息
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (Exception ex)
                {
                    LogHelper.Default.WriteError(ex.Message);
                }

                if (authTicket != null)
                {
                    //反序列化json 放到当前用户信息
                    DeluxeUser.CurrentMember = JsonConvert.DeserializeObject<HPITMemberInfo>(authTicket.UserData);
                    LogHelper.Default.WriteInfo(DeluxeUser.CurrentMember.RealName + "登录了系统" + filterContext.ActionDescriptor.ActionName + "--" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                       {
                                           {"controller","Login"},
                                           {"action","Index"},
                                           {"returnUrl",filterContext.HttpContext.Request.RawUrl }
                                       });
            }
        }
    }
}