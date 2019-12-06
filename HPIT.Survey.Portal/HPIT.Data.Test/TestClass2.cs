using HPIT.Evalute.Data;
using HPIT.StudentEvaluate.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Data.Test
{
    [TestClass]
    public class TestClass2
    {
        [TestMethod]
        public void TestMethod2()
        {
            SurveyContext context = new SurveyContext();
            SkillTag skillTag = new SkillTag() { TagID=Guid.NewGuid().ToString(), TagName = "标签测试", Direction = "NET", CourseName = "ASP.NET" };
            SkillTag skillTag2 = new SkillTag() {TagID=Guid.NewGuid().ToString() , TagName = "标签测试", Direction = "NET", CourseName = "ASP.NET" };
            List<SkillTag> tags = new List<SkillTag>();
            tags.Add(skillTag);
            tags.Add(skillTag2);
            context.SkillTag.AddRange(tags);

            context.SkillTags.Add(new SkillTags() { ID=Guid.NewGuid().ToString(), TagID=skillTag.TagID, PositionID = Guid.NewGuid().ToString() });
            context.SkillTags.Add(new SkillTags() { ID=Guid.NewGuid().ToString(), TagID = skillTag2.TagID, PositionID = Guid.NewGuid().ToString() });
            var result = context.SaveChanges();
            string json = JsonConvert.SerializeObject(result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            SurveyContext context = new SurveyContext();
            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 2, PositionID = 29 });
            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 1, PositionID = 29 });
            context.SaveChanges();
            string json = JsonConvert.SerializeObject(1);
        }


        [TestMethod]
        public void TestMethodMenus()
        {
            var result = MenuDal.Instance.GetMenusByRoleName("学术主管");
            string json = JsonConvert.SerializeObject(result);
        }


        [TestMethod]
        public void TestMethodPTags()
        {
            var result = PositionDal.Instance.GetTagsByPositionID("e8337a42-0d06-4fee-8332-ab227caee5fc");
            string json = JsonConvert.SerializeObject(result);
        }

        [TestMethod]
        public void TestMethod4()
        {

            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 2, PositionID = 29 });
            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 1, PositionID = 29 });
            EvaluteDal dal = new EvaluteDal();
            var result = dal.GetMatchStudent("谢吉龙", "");
            string json = JsonConvert.SerializeObject(result);
        }

        [TestMethod]
        public void TestMethod5()
        {

            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 2, PositionID = 29 });
            //context.SkillTags.Add(new SkillTags() { ID = Guid.NewGuid(), TagID = 1, PositionID = 29 });
            EvaluteDal dal = new EvaluteDal();
            var result = dal.GetMatchStudentByNo("02020160267");
            string json = JsonConvert.SerializeObject(result);
        }


        [TestMethod]
        public void TestMethod6()
        {
            var json = SurveyDal.Instance.DeleteSurvery(1073);
        }

        [TestMethod]
        public void TestMethodLogin()
        {
            EvaluteDal dal = new EvaluteDal();
            string encry = Md5Encrypt.Md5("123456");
            var json = dal.LoginMember("诸葛亮", encry);
        }


        [TestMethod]
        public void TestMethod7()
        {
            AuditLog log = new AuditLog();
            log.AuditName = "王瑞祥";
            log.AuditTime = DateTime.Now;
            log.RoleName = "项目经理";
            log.SurveyID = 55;
            log.AuditState = 1;
            SurveyContext context = new SurveyContext();
            context.AuditLog.Add(log);
            var result = context.SaveChanges();
        }

        [TestMethod]
        public void TestMethod8()
        {
            SurveyContext context = new SurveyContext();
            var result = context.AuditLog.Where(r => r.ID == 1).FirstOrDefault();
        }


        [TestMethod]
        public void TestUpdateTag()
        {
            SkillTagDal dal = new SkillTagDal();
            SkillTag tag = new SkillTag();
            tag.TagID = "2c9a86d9-23a0-4bb0-9859-360613fe0b89";
            tag.TagName = "Egret";
            tag.TagType = "技术";
            tag.CourseName = "C#";
            tag.Direction = "NET";
            int reuslt = dal.UpdateTag(tag);
            var result = dal.context.SkillTag.ToList();
            string json = JsonConvert.SerializeObject(reuslt);
        }

        [TestMethod]
        public void TestAddStudentEval()
        {
            StudentEval studentEval = new StudentEval();
            studentEval.Direction = "NET";
            studentEval.StudentName = "王瑞祥";
            studentEval.StudentNo = "123456";
            studentEval.TeacherPoint = 5;
            studentEval.CreateTime = DateTime.Now;
            studentEval.Score = 10;
            SurveyContext context = new SurveyContext();
            context.StudentEvaluate.Add(studentEval);
            var result = context.SaveChanges();
            string json = JsonConvert.SerializeObject(result);
        }
    }
}
