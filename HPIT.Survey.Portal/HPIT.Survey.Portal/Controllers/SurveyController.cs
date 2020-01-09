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
            result.CurrentRole = DeluxeUser.CurrentMember;
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            if (currentUser.FullName == "项目组组长" || currentUser.FullName == "技术主管" || currentUser.FullName == "项目主管" || currentUser.FullName == "人事主管")
            {
                result.CurrentRole.FullName = "学生";
            }
            return new DeluxeJsonResult(result);
        }


        public DeluxeJsonResult GetAuditLogListByID(int surveyID)
        {
            var result = SurveyDal.Instance.AuditLogList(surveyID);
            var data = from log in result
                       select new
                       {
                           AuditTime = log.AuditTime,
                           AuditName = log.AuditName,
                           AuditState = AuditStateStr(log.AuditState)
                       };
            return new DeluxeJsonResult(new { Data = data });
        }

        public string AuditStateStr(int log)
        {
            if (log == 2)
            {
                return "通过";
            }
            if (log == 4)
            {
                return "拒绝";
            }
            return "";
        }

        public DeluxeJsonResult DeleteSurveyByID(int id)
        {
            var result = SurveyDal.Instance.Delete(id);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult ReplacePEMByID(int id,string pem)
        {
            var result = SurveyDal.Instance.ReplacePEM(id,pem);
            return new DeluxeJsonResult(result);
        }

        [AllowAnonymous]
        public DeluxeJsonResult StartUserNewSurvey()
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.User;
            result.CurrentRole = DeluxeUser.CurrentMember;
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult StartStudentNewSurvey(string stuNo)
        {
            AbstractFormModel<SurveyModel> result = SurveyDal.Instance.StartNewSurvey();
            result.Form.Type = (int)SurveyType.Student;
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            result.CurrentRole = DeluxeUser.CurrentMember;
            if (currentUser.FullName == "项目组组长" || currentUser.FullName == "技术主管" || currentUser.FullName == "项目主管" || currentUser.FullName == "人事主管")
            {
                result.CurrentRole.FullName = "学生";
            }
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
            return new DeluxeJsonResult(result);
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
            return new DeluxeJsonResult(result);
        }

        [HttpPost]
        public DeluxeJsonResult Update(SurveyModel model)
        {
            SurveyDal dal = new SurveyDal();
            SurveyModel match = dal.QueryByID(model.SurveyID).Form;
            match.CompanyNo = model.CompanyNo;
            match.DepartName = model.DepartName;
            match.Direction = model.Direction;
            match.Phone = model.Phone;
            match.PositionName = model.PositionName;
            match.School = model.School;
            match.StudentNo = model.StudentNo;
            match.StuName = model.StuName;
            match.Type = model.Type;
            match.WagesOfFull = model.WagesOfFull;
            match.WagesOfPeriod = model.WagesOfPeriod;
            match.WagesOfReal = model.WagesOfReal;
            match.Year = model.Year;
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
            dal.context.ActiveJobs.RemoveRange(match.ActiveJobs);
            dal.context.Position.RemoveRange(match.Position);
            dal.context.Project.RemoveRange(match.Project);
            dal.context.SaveChanges();
            match.Project = newProject;
            match.Position = newPositions;
            match.ActiveJobs = newJobs;
            string json = JsonConvert.SerializeObject(match);
            int result = dal.Update(match);
            return new DeluxeJsonResult(result);
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
            return new DeluxeJsonResult(result);
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
                if (currentUser.FullName == "项目组组长" || currentUser.FullName == "技术主管" || currentUser.FullName == "项目主管" || currentUser.FullName == "人事主管")
                {
                    item.CurrentRoleName = "学生";
                }
                else
                {
                    item.CurrentRoleName = currentUser.FullName;
                }
            }
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total ,TotalPages = totalPages});
        }

        [AllowAnonymous]
        public DeluxeJsonResult QueryStudentInfo(string name,string className)
        {
            List<EvalStudent> result = EvaluteDal.Instance.GetMatchStudent(name,className);
            if (result.FirstOrDefault() == null)
            {
                return new DeluxeJsonResult(new { Data = result, State = "Fail" });
            }
            else
            {
                return new DeluxeJsonResult(new { Data = result.FirstOrDefault(), State = "OK" });
            }
        }

        [AllowAnonymous]
        public DeluxeJsonResult QueryCompanyByNo(string companyNO)
        {
            var match = SurveyDal.Instance.GetMatchCompany(companyNO);
            return new DeluxeJsonResult(new { Data = match, State = "OK" });
        }

    }
}