using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }

        public DeluxeJsonResult GetCityStatics()
        {
            var result = SurveyDal.Instance.CityStatistic();
            return new DeluxeJsonResult(result);
        }

        public ActionResult IndustryIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetIndustryStatistic()
        {
            var result = CommonDal.Instance.IndustryStatistic();
            return new DeluxeJsonResult(result);
        }


        public ActionResult SalaryIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetSalaryStatistic()
        {
            var result = CommonDal.Instance.SalaryStatistic();
            return new DeluxeJsonResult(result);
        }


        public ActionResult JobIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetJobStatistic()
        {
            var result = CommonDal.Instance.JobStatistic();
            return new DeluxeJsonResult(result);
        }

    }
}