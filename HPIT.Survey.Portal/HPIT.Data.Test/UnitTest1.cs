﻿using System;
using System.Collections.Generic;
using HPIT.Data.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using static HPIT.Survey.Data.Entitys.Enumerations;

namespace HPIT.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SurveyModel survey = new SurveyModel();
            survey.StuName = "调查学生11-29";
            survey.WagesOfFull = 123;
            survey.WagesOfPeriod = 321;
            survey.WagesOfReal = 333;
            survey.SurveyTickNumber = "123456";
            List<Position> posts = new List<Position>();
            posts.Add(new Position() { PositionID=Guid.NewGuid().ToString(),  PositionType = "NET初级工程师"  , PositionDesc = ".NET工程师" ,  PositionName = "NET工程师"});
            posts.Add(new Position() { PositionID=Guid.NewGuid().ToString(), PositionType = "NET高级工程师", PositionDesc = ".NET工程师2", PositionName = "NET工程师2" });
            survey.Position = posts;
            List<ActiveJobs> activeJobs = new List<ActiveJobs>();
            activeJobs.Add(new ActiveJobs() { PositionID=posts[0].PositionID, EndTime = DateTime.Now, StartTime = DateTime.Now, JobDesc = "NET初级工程师", JobName = "2工程师", JobType = "2NET" });
            activeJobs.Add(new ActiveJobs() { PositionID = posts[1].PositionID, EndTime = DateTime.Now, StartTime= DateTime.Now, JobDesc = "NET高级工程师", JobName="工程师", JobType="NET"});
            survey.ActiveJobs = activeJobs;
            List<Project> projects = new List<Project>();
            projects.Add(new Project() { BeginTime = DateTime.Now, EndTime = DateTime.Now, ProjectDesc ="第一个项目", ProjectName ="物流管理系统" , ProjectType ="物流", ProjectTypeID = 1 });
            projects.Add(new Project() { BeginTime = DateTime.Now, EndTime = DateTime.Now, ProjectDesc = "2第一个项目", ProjectName = "物流管理系统2", ProjectType = "物流2", ProjectTypeID = 1 });
            survey.Project = projects;
            survey.CompanyNo = "114105022468253";
            survey.Company.CompanyDesc = "这是公司描述";
            survey.Company.CompanyType = "计算机互联网";
            survey.Company.CompanyName = "腾讯";
            survey.Company.City = "深圳";
            survey.Year = 2019;
            survey.CreateTime = DateTime.Now;
            survey.Direction = "Net";
            survey.Batch = "Net190901";
            survey.ProjectName = "Net项目部";
            survey.Phone = "17700611332";
            survey.CorworkPhone = "18813048831";
            survey.Status = (int)SurveyStatus.draft;
            //survey.Student = new Student() { Batch="NET190901", Address ="中兴", City="郑州", Phone = "17700611332", StuName = "王瑞祥"  , Year = 2019 }
            SurveyDal.Instance.Create(survey);
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = survey;

            string result = JsonConvert.SerializeObject(model);
        }

        [TestMethod]
        public void TestDelete()
        {
            var result = SurveyDal.Instance.DeleteSurvery(56);
            string ss = JsonConvert.SerializeObject(result);
        }


        [TestMethod]
        public void TestMethod2()
        {
            Student stu = new Student();
            stu.Year = 2019;
            stu.StuName = "王瑞祥";
            stu.Phone = "17700611332";
            stu.City = "北京";
            stu.Batch = "NET190901";
            SurveyContext.Instance.Student.Add(stu);
            SurveyContext.Instance.SaveChanges();
            string result = JsonConvert.SerializeObject(stu);
        }


        [TestMethod]
        public void TestMethod4()
        {

            Company stu = new Company();
            stu.CompanyNo = "2019";
            stu.CompanyName = "河南厚溥有限公司";
            stu.CompanyType = "互联网信息服务";
            stu.City = "北京";
            stu.CompanyDesc = "NET190901";
            SurveyContext.Instance.Company.Add(stu);
            SurveyContext.Instance.SaveChanges();
            string result = JsonConvert.SerializeObject(stu);
        }


        [TestMethod]
        public void TestMethod3()
        {
            var rsult = SurveyDal.Instance.QueryByID(11);
            string result = JsonConvert.SerializeObject(rsult);
        }

        [TestMethod]
        public void TestMethodPage()
        {
            SearchModel<SurveyModel> search = new SearchModel<SurveyModel>();
            search.PageIndex = 0;
            search.PageSize = 10;
            int total = 0;
            var rsult = SurveyDal.Instance.GetPageData(search,out total);
            string result = JsonConvert.SerializeObject(rsult);
        }

        [TestMethod]
        public void TestMethodPositionPage()
        {
            SearchModel<PositionExt> search = new SearchModel<PositionExt>();
            search.PageIndex = 0;
            search.PageSize = 10;
            int total = 0;
            var rsult = PositionDal.Instance.GetPageData(search, out total);
            string result = JsonConvert.SerializeObject(rsult);
        }


        [TestMethod]
        public void TestMethodPositionAdd()
        {

            Position stu = new Position();
            stu.PositionName = "测试工程师";
            stu.PositionType = "测试";
            SurveyContext context = new SurveyContext();
            var result = context.Position.Add(stu);
            context.SaveChanges();
            string json = JsonConvert.SerializeObject(result);
        }

        [TestMethod]
        public void TestMethodUpdate()
        {
            SurveyDal dal = new SurveyDal();
            //首先根据id 去查询数据
            var rsult = dal.QueryByID(1076);
            rsult.Form.CorworkPhone = "17700923232";
            rsult.Form.Direction = "大数据";
            rsult.Form.ProjectName = "修改";
            string json = JsonConvert.SerializeObject(rsult.Form);
            List<Project> newProject = new List<Project>();
            //foreach (var item in rsult.Form.Project)
            //{
            //    newProject.Add(item);
            //}
            List<Position> newPositions = new List<Position>();
            //foreach (var item in rsult.Form.Position)
            //{
            //    newPositions.Add(item);
            //}
            List<ActiveJobs> newJobs = new List<ActiveJobs>();
            //foreach (var item in rsult.Form.ActiveJobs)
            //{
            //    newJobs.Add(item);
            //}
            dal.context.Position.RemoveRange(rsult.Form.Position);
            dal.context.Project.RemoveRange(rsult.Form.Project);
            dal.context.ActiveJobs.RemoveRange(rsult.Form.ActiveJobs);
            dal.context.SaveChanges();
            //添加子表 要跟新添加的数据。
            newPositions.Add(new Position() { PositionID=Guid.NewGuid().ToString(), PositionName="测试工程师4" });
            newPositions.Add(new Position() { PositionID = Guid.NewGuid().ToString(), PositionName = "测试工程师5" });
            newProject.Add(new Project() {  ProjectName = "update5", BeginTime = DateTime.Now,EndTime=DateTime.Now, ProjectType = "ee",SurveyID = rsult.Form.SurveyID});
            newProject.Add(new Project() { ProjectName = "update6", BeginTime = DateTime.Now, EndTime = DateTime.Now, ProjectType = "ee", SurveyID = rsult.Form.SurveyID });
            rsult.Form.Project = newProject;
            rsult.Form.Position = newPositions;
            rsult.Form.ActiveJobs = newJobs;
            int tt = dal.Update(rsult.Form);
            string result = JsonConvert.SerializeObject(rsult.Form);
        }

        [TestMethod]
        public void TestSurveyUpdate()
        {
            SurveyDal dal = new SurveyDal();
            var rsult = dal.QueryByID(1076);
            rsult.Form.CorworkPhone = "17700923232";
            rsult.Form.Direction = "大数据2";
            rsult.Form.ProjectName = "修改2";
            rsult.Form.Project.Add(new Project() { ProjectName="酒店管理系统", ProjectType="酒店管理" });
            int tt = dal.Update(rsult.Form);
            string result = JsonConvert.SerializeObject(rsult);
        }
    }
}
