using HPIT.Data.Core;
using HPIT.Evalute.Data;
using HPIT.Evalute.Data.Model;
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

        public DeluxeJsonResult DeleteSurveyByID(int id)
        {
            var result = SurveyDal.Instance.Delete(id);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult StartUserNewSurvey()
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.User;
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult StartStudentNewSurvey(string stuNo)
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.Student;
            //根据学生编号创建新的
            List<EvalStudent> matchList = EvaluteDal.Instance.GetMatchStudentByNo(stuNo);
            if (matchList.Count == 1)
            {
                EvalStudent match = matchList.FirstOrDefault();
                result.Form.ProjectName = match.pName;
                result.Form.Phone = match.Mobile;
                result.Form.Direction = match.mName;
                result.Form.Year = match.bYear;
                result.Form.School = match.GraduateSchool;
                result.Form.Batch = match.bName;
                result.Form.PRM = match.PRM;
                result.Form.PEM = match.PEM;
            }
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        [HttpPost]
        public DeluxeJsonResult Save(SurveyModel model)
        {
            List<EvalStudent> matchList = EvaluteDal.Instance.GetMatchStudent(model.StuName, "");
            if (matchList.Count == 1)
            {
                EvalStudent match = matchList.FirstOrDefault();
                model.PRM = !string.IsNullOrEmpty(model.PRM) ? model.PRM: match.PRM;
                model.PEM = !string.IsNullOrEmpty(model.PEM) ? model.PEM : match.PEM;
            }
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

        public DeluxeJsonResult QueryStudentInfo(string name,string className)
        {
            List<EvalStudent> result = EvaluteDal.Instance.GetMatchStudent(name,className);
            if (result.FirstOrDefault() == null)
            {
                return new DeluxeJsonResult(new { Data = result, State = "Fail" }, "yyyy-MM-dd HH:mm");
            }
            else
            {
                return new DeluxeJsonResult(new { Data = result.FirstOrDefault(), State = "OK" }, "yyyy-MM-dd HH:mm");
            }
            
        }

    }
}