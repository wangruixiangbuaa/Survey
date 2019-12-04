using HPIT.Evalute.Data;
using HPIT.Evalute.Data.Model;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Portal.Common;
using HPIT.Survey.Portal.Filters;
using HPIT.Survey.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> InitPoints()
        {
            List<SelectListItem> points = new List<SelectListItem>();
            for (int i = 1; i <= 10; i++)
            {
                points.Add(new SelectListItem() { Text = i.ToString(),Value = i.ToString() });
            }
            return points;
        }

        [HttpPost]
        public DeluxeJsonResult AddEvalPoint(StudentTagsModel model)
        {
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            string stuNo = "";
            if (currentUser.FullName != "学生")
            {
                return new DeluxeJsonResult(new { data="只能学生进行技术评估",status=403 }, "yyyy-MM-dd HH:mm");
            }
            List<EvalStudent> matchList = EvaluteDal.Instance.GetMatchStudent(currentUser.RealName, "");
            if (matchList.Count == 1)
            {
                EvalStudent match = matchList.FirstOrDefault();
                stuNo = match.StudentNo;
            }
            foreach (var stag in model.tags)
            {
                stag.StudentName = currentUser.RealName;
                stag.StudentNo = stuNo;
            }
            var result = StudentDal.Instance.AddStudentTags(stuNo,model.tags);
            return new DeluxeJsonResult(new { data = result, status = 200 }, "yyyy-MM-dd HH:mm");
        }

        [HttpPost]
        public DeluxeJsonResult GetDirectionSkillTag(string direction)
        {
            var tags = SkillTagDal.Instance.DirectionTags(direction);
            return new DeluxeJsonResult(new { data=tags , points = InitPoints() }, "yyyy-MM-dd HH:mm");
        }
    }
}