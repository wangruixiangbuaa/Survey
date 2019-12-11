using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public DeluxeJsonResult GetProjectStatics()
        {
            var result = ProjectDal.Instance.ProjectStatistic();
            return new DeluxeJsonResult(result);
        }
    }
}