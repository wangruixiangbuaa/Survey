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
        public ActionResult CityIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetCityStatics(string position)
        {
            var result = CommonDal.Instance.CityStatistic(position);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetCityStaticsDetail(string position,string city)
        {
            var result = CommonDal.Instance.CityStatisticDetail(position,city);
            return new DeluxeJsonResult(new { Data = result });
        }

        public ActionResult IndustryIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetIndustryStatistic(string position)
        {
            var result = CommonDal.Instance.IndustryStatistic(position);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetIndustryStatisticDetail(string position, string type)
        {
            var result = CommonDal.Instance.IndustryStatisticDetail(position,type);
            return new DeluxeJsonResult(new { Data = result });
        }

        public DeluxeJsonResult GetAllPositionName()
        {
            var result = CommonDal.Instance.GetAllPositionNames();
            return new DeluxeJsonResult(new { Data = result });
        }

        public ActionResult SalaryIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetSalaryStatistic(string position)
        {
            var result = CommonDal.Instance.SalaryStatistic(position);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetSalaryStatisticDetail(string position,string range)
        {
            var result = CommonDal.Instance.SalaryStatisticDetail(position,range);
            return new DeluxeJsonResult(new { Data = result });
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

        public DeluxeJsonResult GetAllPositionTbName()
        {
            var result = CommonDal.Instance.GetAllPositionTbNames();
            return new DeluxeJsonResult(new { Data = result });
        }


        public ActionResult SkillStatisticIndex()
        {
            return View();
        }

        public DeluxeJsonResult GetSkillStatistic(string position)
        {
            var result = CommonDal.Instance.SkillStatistic(position);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetSkillStatisticDetail(string position, string tagName)
        {
            var result = CommonDal.Instance.SkillStatisticDetail(position, tagName);
            return new DeluxeJsonResult(new { Data = result });
        }

    }
}