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

        public DeluxeJsonResult GetProjectStatics(string position)
        {
            var result = ProjectDal.Instance.ProjectStatistic(position);
            return new DeluxeJsonResult(result);
        }


        public DeluxeJsonResult GetProjectCityStatics(string city)
        {
            var result = ProjectDal.Instance.ProjectCityStatistic(city);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetProjectStaticsDetail(string position, string type)
        {
            var result = ProjectDal.Instance.GetProjectListByType(position,type);
            return new DeluxeJsonResult(new { Data = result });
        }


        public DeluxeJsonResult GetAllPositionName()
        {
            var result = ProjectDal.Instance.GetAllPositionNames();
            return new DeluxeJsonResult(new { Data = result });
        }

    }
}