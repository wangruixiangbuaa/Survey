using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPIT.Survey.Data.ExtEntitys;
using System.Data.Entity;
using HPIT.Data.Core;

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
            context.Entry(survey).State = EntityState.Modified;
            return context.SaveChanges();
        }


        public int Audit(AuditLog log)
        {
            log.AuditTime = DateTime.Now;
            SurveyContext context = new SurveyContext();
            context.AuditLog.Add(log);
            context.Database.ExecuteSqlCommand(
                  string.Format(@"UPDATE dbo.SurveyModel SET AuditName = '{0}', AuditStatus={1} where SurveyID={2}",log.AuditName,log.AuditState,log.SurveyID));
            return context.SaveChanges();
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
            model.Form.Student = new Student();
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
            List<GeneralSelectItem> industrys = context.Dictionary.Where(r => r.Type == "行业").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            industrys.Insert(0,new GeneralSelectItem() { Text="请选择",Value=""});
            ExtraDatas["Industrys"] = industrys;
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

        /// <summary>
        /// 分页查询，代码
        /// </summary>
        /// <param name="search"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public object GetPageData(SearchModel<SurveyModel> search,out int count)
        {
            GetPageListParameter<SurveyModel, int> parameter = new GetPageListParameter<SurveyModel, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.SurveyID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.Status != 1;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<SurveyModel> list = baseService.GetSimplePagedData<SurveyModel, int>(parameter, out count);
            return list;
        }
    }
}
