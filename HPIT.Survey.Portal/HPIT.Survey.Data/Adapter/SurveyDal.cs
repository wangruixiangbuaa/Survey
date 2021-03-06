﻿using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPIT.Survey.Data.ExtEntitys;
using System.Data.Entity;
using HPIT.Data.Core;
using System.Linq.Expressions;
using HPIT.Survey.Data.Tool;
using static HPIT.Survey.Data.Entitys.Enumerations;

namespace HPIT.Survey.Data.Adapter
{
    public class SurveyDal
    {
        public static SurveyDal Instance = new SurveyDal();

        public SurveyContext context { get; set; }
        public SurveyDal()
        {
            this.context = new SurveyContext();
        }

        /// <summary>
        /// 创建调查问卷
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        public int Create(SurveyModel survey)
        {
            SurveyContext context = new SurveyContext();
            survey.CreateTime = DateTime.Now;
            survey.AuditStatus = (int)SurveyStatus.audit;
            //匹配公司信息
            Company match = context.Company.FirstOrDefault(r => r.CompanyNo == survey.CompanyNo);
            if (match != null)
            {
                survey.CompanyID = match.CompanyID;
                match.City = survey.Company.City;
                match.CompanyDesc = survey.Company.CompanyDesc;
                match.CompanyType = survey.Company.CompanyType;
                match.CompanyDetailType = survey.Company.CompanyDetailType;
                survey.City = match.City;
                survey.Company = match;
            }
            else
            {
                survey.City = survey.Company.City;
                survey.Company.CompanyNo = survey.CompanyNo;
                context.Company.Add(survey.Company);
                context.SaveChanges();
                survey.CompanyID = survey.Company.CompanyID;
            }
            context.SurveyModel.Add(survey);
            //遍历所有的职位，生成标签
            List<SkillTag> allTags = context.SkillTag.ToList();
            List<SkillTags> matchTags = new List<SkillTags>();
            try
            {
                foreach (Position position in survey.Position)
                {
                    foreach (SkillTag tag in allTags)
                    {
                        if (!string.IsNullOrEmpty(position.PositionDesc))
                        {
                            string desc = position.PositionDesc.ToLower().Trim();
                            string tagLower = tag.TagName.ToLower();
                            if (desc.Contains(tagLower))
                            {
                                SkillTags matchTag = new SkillTags();
                                matchTag.ID = Guid.NewGuid().ToString();
                                matchTag.PositionID = position.PositionID;
                                matchTag.TagID = tag.TagID;
                                matchTags.Add(matchTag);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //保存的时候进行验证
            context.Configuration.ValidateOnSaveEnabled = false;
            //自动添加匹配的职位标签
            context.SkillTags.AddRange(matchTags);
            return context.SaveChanges();
        }

        /// <summary>
        /// 更新调查问卷
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        public int Update(SurveyModel survey)
        {
            survey.CreateTime = DateTime.Now;
            survey.AuditStatus = (int)SurveyStatus.audit;
            //匹配公司信息
            Company match = context.Company.FirstOrDefault(r => r.CompanyNo == survey.CompanyNo);
            if (match != null)
            {
                survey.CompanyID = match.CompanyID;
                match.City = survey.Company.City;
                match.CompanyDesc = survey.Company.CompanyDesc;
                match.CompanyType = survey.Company.CompanyType;
                match.CompanyDetailType = survey.Company.CompanyDetailType;
                survey.City = match.City;
                survey.Company = match;
            }
            else
            {
                survey.City = survey.Company.City;
                survey.Company.CompanyNo = survey.CompanyNo;
            }
            context.Entry(survey).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int DeleteSurvery(int surveyID)
        {
            //根据id去查询主表的数据
            SurveyModel match = context.SurveyModel.FirstOrDefault(r => r.SurveyID == surveyID);
            //找到匹配的数据
            if (match != null)
            {
                //Position 一对多的
                foreach (var item in match.Position)
                {
                    context.SkillTags.RemoveRange(item.SkillTags);
                }
                //
                context.ActiveJobs.RemoveRange(match.ActiveJobs);
                context.Position.RemoveRange(match.Position);
                context.Project.RemoveRange(match.Project);
                context.Entry(match).State = EntityState.Deleted;
            }
            return context.SaveChanges();
        }


        public int Audit(AuditLog log)
        {
            log.AuditTime = DateTime.Now;
            SurveyContext context = new SurveyContext();
            //通过工作流审批 获取到下一个审批人
            string nextAuditName = "";
            SurveyWorkFlow surveyWorkFlow = new SurveyWorkFlow();
            int finalLogStatus = 0;
            //审批通过
            if (log.AuditState == 1)
            {
                nextAuditName = surveyWorkFlow.Pass(log.AuditName, log.RoleName);
                log.AuditState = (int)SurveyStatus.pass;
                finalLogStatus = log.AuditState;
            }
            //拒绝
            else
            {
                nextAuditName = surveyWorkFlow.Reject(log.AuditName, log.RoleName);
                log.AuditState = (int)SurveyStatus.reject;
                finalLogStatus = log.AuditState;
                if (nextAuditName == "人事经理")
                {
                    SurveyModel current = this.QuerySingleByID(log.SurveyID).Form;
                    //如果没找到则为空，如果不为空，则设置成为当前单子的pem;
                    nextAuditName = current == null ? "" : current.PEM;
                }

                if (nextAuditName == "学生")
                {
                    finalLogStatus = (int)SurveyStatus.draft;
                    nextAuditName = "";
                }
            }
            if (nextAuditName == "已完成")
            {
                log.AuditState = (int)SurveyStatus.complete;
            }
            //添加一条log
            context.AuditLog.Add(log);
            context.Database.ExecuteSqlCommand(
                  string.Format(@"UPDATE dbo.SurveyModel SET AuditName = '{0}', AuditStatus={1} where SurveyID={2}", nextAuditName, finalLogStatus ,log.SurveyID));
            return context.SaveChanges();
        }

        public int ReplacePEM(int id, string pem)
        {
            SurveyContext context = new SurveyContext();
            context.Database.ExecuteSqlCommand(
                   string.Format(@"UPDATE dbo.SurveyModel SET PEM = '{0}' where SurveyID={1}", pem,id));
            var result = context.SaveChanges();
            return result;
        }


        public List<AuditLog> AuditLogList(int surveyID)
        {
            var logs = context.AuditLog.Where(r => r.SurveyID == surveyID && (r.AuditState == 2 || r.AuditState == 4)).OrderByDescending(r=>r.AuditTime).ToList();
            return logs;
        }


        /// <summary>
        /// 根据id删除调查问卷 软删除
        /// </summary>
        /// <param name="surveyID"></param>
        /// <returns></returns>
        public int Delete(int surveyID)
        {
            int result = 0;
            using (var context = new SurveyContext())
            {
               result = context.Database.ExecuteSqlCommand(
                  "UPDATE dbo.SurveyModel SET Status = 1 where SurveyID=" + surveyID);
            }
            return result;
        }

        public void GetList()
        {
            //using (var context = new MyDBContext())
            //{
            //    var postTitles = context.Database.SqlQuery<string>("SELECT Title FROM dbo.Posts").ToList();
            //}
        }

        public AbstractFormModel<SurveyModel> StartNewSurvey()
        {
            SurveyContext context = new SurveyContext();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = new SurveyModel();
            model.Form.Company = new Company();
            model.ExtraDatas = GetExtraDatas();
            return model;
        }

        public Dictionary<string, object> GetExtraDatas()
        {

            Dictionary<string, object> ExtraDatas = new Dictionary<string, object>();
            List<GeneralSelectItem> directions = context.Dictionary.Where(r => r.Type == "方向").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            directions.Insert(0,new GeneralSelectItem() { Text="请选择",Value=""});
            ExtraDatas["Directions"] = directions;
            List<GeneralSelectItem> citys = context.Dictionary.Where(r => r.Type == "城市").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            citys.Insert(0,new GeneralSelectItem() { Text = "请选择", Value="" });
            ExtraDatas["Citys"] = citys;
            List<GeneralSelectItem> projectTypes = context.Dictionary.Where(r => r.Type == "项目类型").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            projectTypes.Insert(0,new GeneralSelectItem() { Text="请选择" ,Value=""});
            ExtraDatas["ProjectTypes"] = projectTypes;
            List<GeneralSelectItem> sources = context.Dictionary.Where(r => r.Type == "来源").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            sources.Insert(0,new GeneralSelectItem() { Text = "请选择" , Value=""});
            ExtraDatas["Source"] = sources;
            List<GeneralSelectItem> industrys = context.Dictionary.Where(r => r.Type == "行业" && r.ParentID == null).Select(r => new GeneralSelectItem { ID=r.ID, Text = r.Name, Value = r.Value }).ToList();
            industrys.Insert(0,new GeneralSelectItem() { Text="请选择",Value=""});
            ExtraDatas["Industrys"] = industrys;
            List<GeneralSelectItem> detailindustrys = context.Dictionary.Where(r => r.Type == "行业" && r.ParentID >0).Select(r => new GeneralSelectItem {ID = r.ID, Text = r.Name, Value = r.Value, parentID = (int)r.ParentID }).ToList();
            detailindustrys.Insert(0, new GeneralSelectItem() { Text = "请选择", Value = "" });
            ExtraDatas["DetailIndustrys"] = detailindustrys;
            List<GeneralSelectItem> secondindustrys = new List<GeneralSelectItem>();
            detailindustrys.Insert(0, new GeneralSelectItem() { Text = "请选择", Value = "" });
            ExtraDatas["SecondIndustrys"] = secondindustrys;
            ExtraDatas["Count"] = CommonDal.Instance.GetCount();
            ExtraDatas["Years"] = CommonDal.Instance.GetYears();
            return ExtraDatas;
        }

        public AbstractFormModel<SurveyModel> QueryByID(int id)
        {
            var survey = context.SurveyModel.Where(r => r.SurveyID == id).FirstOrDefault();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = survey;
            model.ExtraDatas = GetExtraDatas();
            return model;
        }


        public AbstractFormModel<SurveyModel> QuerySingleByID(int id)
        {
            SurveyContext db = new SurveyContext();
            var survey = db.SurveyModel.Where(r => r.SurveyID == id).FirstOrDefault();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = survey;
            model.ExtraDatas = GetExtraDatas();
            return model;
        }

        public Company GetMatchCompany(string No)
        {
            var match = context.Company.Where(r => r.CompanyNo == No).FirstOrDefault();
            return match;
        }

        /// <summary>
        /// 分页查询，代码
        /// </summary>
        /// <param name="search"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<SurveyModel> GetPageData(SearchModel<SurveyModel> search,out int count)
        {
            GetPageListParameter<SurveyModel, int> parameter = new GetPageListParameter<SurveyModel, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.SurveyID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            if (search.RoleName == "学生" || search.RoleName == "项目组组长" || search.RoleName == "技术主管" || search.RoleName == "项目主管" || search.RoleName == "人事主管")
            {
                Expression<Func<SurveyModel, bool>> lambda0 = item => item.Status != 1;
                Expression<Func<SurveyModel, bool>> lambda1 = item => item.StuName == search.UserName;
                //合并表达式
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(lambda0, lambda1);
            }
            else if (search.RoleName == "人事经理")
            {
                Expression<Func<SurveyModel, bool>> lambda0 = item => item.Status != 1;
                Expression<Func<SurveyModel, bool>> lambda1 = item => item.PEM == search.UserName && (item.AuditName == item.PEM || string.IsNullOrEmpty(item.AuditName) || item.AuditStatus == 3);
                //合并表达式
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(lambda0, lambda1);
            }
            else if (search.RoleName == "项目经理")
            {
                Expression<Func<SurveyModel, bool>> lambda0 = item => item.Status != 1;
                Expression<Func<SurveyModel, bool>> lambda1 = item => item.PRM == search.UserName;
                //合并表达式
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(lambda0, lambda1);
            }
            else
            {
                Expression<Func<SurveyModel, bool>> lambda0 = item => item.Status != 1;
                Expression<Func<SurveyModel, bool>> lambda1 = item => item.AuditName == search.UserName || item.AuditStatus == 3;
                //合并表达式
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(lambda0, lambda1);
            }
            if (!string.IsNullOrEmpty(search.Entity.Batch))
            {
                Expression<Func<SurveyModel, bool>> batchWhere = item => item.Batch == search.Entity.Batch;
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(parameter.whereLambda, batchWhere);
            }
            if (!string.IsNullOrEmpty(search.Entity.ProjectName))
            {
                Expression<Func<SurveyModel, bool>> projectNameWhere = item => item.ProjectName == search.Entity.ProjectName;
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(parameter.whereLambda, projectNameWhere);
            }
            if (!string.IsNullOrEmpty(search.Entity.Direction))
            {
                Expression<Func<SurveyModel, bool>> directionWhere = item => item.Direction == search.Entity.Direction;
                parameter.whereLambda = ExpressionExt.ReBuildExpression<SurveyModel>(parameter.whereLambda, directionWhere);
            }
            //查询数据
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<SurveyModel> list = baseService.GetSimplePagedData<SurveyModel, int>(parameter, out count);
            return list;
        }
    }
}
