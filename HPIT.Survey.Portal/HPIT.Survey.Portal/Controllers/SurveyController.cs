using HPIT.Data.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using HPIT.Survey.Portal.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HPIT.Survey.Data.Entitys.Enumerations;

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
            ViewBag.Type = (int)SurveyType.User;
            return View();
        }

        public ActionResult StudentIndex()
        {
            ViewBag.Type = (int)SurveyType.Student;
            return View();
        }


        public DeluxeJsonResult GetSurveyByID(int id)
        {
            var result = SurveyDal.Instance.QueryByID(id);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult StartUserNewSurvey()
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.User;
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult StartStudentNewSurvey()
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.Student;
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        [HttpPost]
        public DeluxeJsonResult Save(SurveyModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            var result = SurveyDal.Instance.Create(model);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult Update(SurveyModel model)
        {
            var result = SurveyDal.Instance.Update(model);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult QueryPageData(SearchModel<SurveyModel> search)
        {
            int total = 0;
            var result = SurveyDal.Instance.GetPageData(search,out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total ,TotalPages = totalPages}, "yyyy-MM-dd HH:mm");
        }
    }
}