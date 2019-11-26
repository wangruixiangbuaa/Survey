using HPIT.Evalute.Data;
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
        public void TestUpdateTag()
        {
            SkillTagDal dal = new SkillTagDal();
            SkillTag tag = new SkillTag();
            tag.TagID = "1380bc2a-621d-48da-a531-0b8a9af49b7f";
            tag.TagName = "WebForm2";
            tag.TagType = "技术";
            tag.CourseName = "C#";
            tag.Direction = "NET";
            int reuslt = dal.UpdateTag(tag);
            var result = dal.context.SkillTag.ToList();
            string json = JsonConvert.SerializeObject(reuslt);
        }
    }
}
