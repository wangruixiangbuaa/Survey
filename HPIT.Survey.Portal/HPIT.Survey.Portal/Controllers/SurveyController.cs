using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserIndex()
        {
            return View();
        }


        public DeluxeJsonResult GetSurveyByID(int id)
        {
            //JsonResult result = new JsonResult();
            //result.Data =  
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            //return Content(JsonConvert.SerializeObject(Date, Formatting.Indented, timeConverter));
            var result = SurveyDal.Instance.QueryByID(id);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult StartNewSurvey()
        {
            var result = SurveyDal.Instance.StartNewSurvey();
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult Save(SurveyModel model)
        {
            var result = SurveyDal.Instance.Create(model);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult Update(SurveyModel model)
        {
            var result = SurveyDal.Instance.Create(model);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }
    }
}