using HPIT.Data.Core;
using HPIT.Evalute.Data;
using HPIT.Evalute.Data.Model;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using HPIT.Survey.Portal.Common;
using HPIT.Survey.Portal.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        public DeluxeJsonResult GetSurveyByID(int id)
        {
            var result = SurveyDal.Instance.QuerySingleByID(id);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        public DeluxeJsonResult DeleteSurveyByID(int id)
        {
            var result = SurveyDal.Instance.Delete(id);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }

        [AllowAnonymous]
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
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            //根据学生编号创建新的
            List<EvalStudent> matchList = EvaluteDal.Instance.GetMatchStudent(currentUser.RealName,"");
            if (matchList.Count == 1)
            {
                EvalStudent match = matchList.FirstOrDefault();
                result.Form.ProjectName = match.pName;
                result.Form.StudentNo = match.StudentNo;
                result.Form.StuName = match.Name;
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

        [AllowAnonymous]
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

        [HttpPost]
        public DeluxeJsonResult Update(SurveyModel model)
        {
            SurveyDal dal = new SurveyDal();
            SurveyModel match = dal.QueryByID(model.SurveyID).Form;
            List<Project> newProject = new List<Project>();
            foreach (var item in model.Project)
            {
                item.SurveyID = model.SurveyID;
                newProject.Add(item);
            }
            List<Position> newPositions = new List<Position>();
            foreach (var item in model.Position)
            {
                item.SurveyID = model.SurveyID;
                newPositions.Add(item);
            }
            List<ActiveJobs> newJobs = new List<ActiveJobs>();
            foreach (var item in model.ActiveJobs)
            {
                item.SurveyID = model.SurveyID;
                newJobs.Add(item);
            }
            dal.context.Position.RemoveRange(match.Position);
            dal.context.Project.RemoveRange(match.Project);
            dal.context.ActiveJobs.RemoveRange(match.ActiveJobs);
            dal.context.SaveChanges();
            match.Project = newProject;
            match.Position = newPositions;
            match.ActiveJobs = newJobs;
            string json = JsonConvert.SerializeObject(match);
            int result = dal.Update(match);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }


        public DeluxeJsonResult Audit(int surveyID,int type)
        {
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            AuditLog log = new AuditLog();
            log.AuditName = currentUser.RealName;
            log.AuditState = type;
            log.SurveyID = surveyID;
            log.RoleName = currentUser.FullName;
            SurveyDal dal = new SurveyDal();
            var result = dal.Audit(log);
            return new DeluxeJsonResult(result, "yyyy-MM-dd HH:mm");
        }
        public DeluxeJsonResult QueryPageData(SearchModel<SurveyModel> search)
        {
            int total = 0;
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            search.UserName = currentUser.RealName;
            search.RoleName = currentUser.FullName;
            var result = SurveyDal.Instance.GetPageData(search,out total);
            foreach (var item in result)
            {
                item.CurrentRoleName = currentUser.FullName;
            }
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