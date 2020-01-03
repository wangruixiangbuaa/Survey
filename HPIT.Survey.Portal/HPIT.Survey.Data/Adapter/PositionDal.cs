using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HPIT.Survey.Data.QueryModel;

namespace HPIT.Survey.Data.Adapter
{
    public class PositionDal
    {
        public static PositionDal Instance = new PositionDal();

        public SurveyContext context { get; set; }
        public PositionDal()
        {
            this.context = new SurveyContext();
        }


        public List<CommonStatistic> PositionStatistic(string direction)
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = string.Format(@"select PositionName name ,COUNT(PositionName) value FROM[SurveyDB].[dbo].[SurveyModel] where Direction = '{0}' group by PositionName", direction);
            //string sql = string.Format(@"select PositionType name ,COUNT(PositionType) value from [SurveyDB].[dbo].[Position] p 
            //                             left join [SurveyDB].[dbo].SurveyModel s on p.SurveyID=s.SurveyID where Direction ='{0}' group by PositionType",direction);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }

        public List<Company> GetPositionRelateCompany(string position,string direction)
        {
            List<Company> Statistic = new List<Company>();
            string sql = string.Format(@"select s.StuName,s.Direction,s.Phone ,s.PositionName, c.* FROM [SurveyDB].[dbo].[SurveyModel] s left join [SurveyDB].[dbo].Company c
                                         on s.CompanyID = c.CompanyID
                                         where PositionName='{0}' and Direction='{1}'", position,direction);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<Company>(sql).ToList();
            }
            return Statistic;
        }

        public List<SkillTag> GetTagsByPositionID(string positionID)
        {
            List<SkillTag> tags = new List<SkillTag>();
            using (var context = new SurveyContext())
            {
                string sql = string.Format(@"SELECT s.PositionID,t.*
                                             FROM [SurveyDB].[dbo].[SkillTags] s 
                                             left join [SurveyDB].[dbo].[SkillTag] t on s.TagID = t.TagID 
                                             where PositionID = '{0}'", positionID);
                tags = context.Database.SqlQuery<SkillTag>(sql).ToList();
            }
            return tags;
        }

        public object GetPageData(SearchModel<PositionExt> search, out int count)
        {
            GetPageListParameter<PositionExt, int> parameter = new GetPageListParameter<PositionExt, int>();
            parameter.isAsc = true;
            //parameter.orderByLambda = t => t.PositionID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            //parameter.whereLambda = t => t.PositionID > 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            string sqlwhere = " where 1=1 ";
            string sql = @"SELECT p.*,c.CompanyName ,c.CompanyType 
                         FROM dbo.[Position] p 
                         left join dbo.[SurveyModel] s on p.SurveyID = s.SurveyID 
                         left join dbo.[Company] c on c.CompanyID = s.CompanyID";
            if (!string.IsNullOrEmpty(search.Entity.PositionType))
            {
                sqlwhere += string.Format(" and PositionType='{0}'",search.Entity.PositionType);
            }
            if (!string.IsNullOrEmpty(search.Entity.Source))
            {
                sqlwhere += string.Format(" and Source ='{0}'",search.Entity.Source);
            }
            sql = string.Format("{0}{1}",sql,sqlwhere);
            List<PositionExt> list = baseService.GetSqlPagedData<PositionExt, int>(sql,parameter, out count);
            foreach (PositionExt position in list)
            {
                //查询对应职位的标签。
                position.Tags = this.GetTagsByPositionID(position.PositionID);
                if (position.Tags.Count > 0)
                {
                    position.TagsJsonStr = JsonConvert.SerializeObject(position.Tags.Select(r => r.TagName).ToList());
                }
            }
            return list;
        }


        public object GetMatchPageData(SearchModel<PositionMatchModel> search, out int count)
        {
            GetPageListParameter<PositionMatchModel, int> parameter = new GetPageListParameter<PositionMatchModel, int>();
            parameter.isAsc = true;
            //parameter.orderByLambda = t => t.PositionID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            //parameter.whereLambda = t => t.PositionID > 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            string sql = @"select * from (SELECT ISNULL(aj.AvageMoney,0) as AvageMoney,tt.Skills,(select count(*) from dbo.SkillTags st where st.PositionID = p.PositionID) skillCount,aj.StartTime,p.*,c.CompanyName,c.City,c.CompanyType 
                         FROM dbo.ActiveJobs aj 
						 left join  dbo.[Position] p  on aj.PositionID = p.PositionID
                         left join (select
                         st.PositionID,Skills = (
                                 stuff(
                                     (select ',' + TagName from SkillTags tst left join SkillTag t on tst.TagID=t.TagID  where  PositionID = st.PositionID  order by t.TagName for xml path('')),
                                     1,
                                     1,
                                     ''
                                 )
                             )
                         from SkillTags as st group by  st.PositionID) tt on tt.PositionID = p.PositionID
                         left join dbo.[SurveyModel] s on p.SurveyID = s.SurveyID 
                         left join dbo.[Company] c on c.CompanyID = s.CompanyID) t";
            string sqlWhere = " where 1=1 ";
            int skillcount = 0;
            string studentTagStr = "";
            List<string> tags = new List<string>();
            if (search.Entity.Point > 0)
            {
                var matchs = context.StudentTags.Where(r => r.StudentName == search.UserName && r.TeacherPoint > search.Entity.Point).ToList();
                skillcount = matchs.Count();
                tags = matchs.OrderBy(r=>r.SkillTag.TagName).Select(r => r.SkillTag.TagName).ToList() ;
                studentTagStr = string.Join(",", tags);
            }
            int totalSkillNeed = 0;
            float realSkillNeed = 0;
            if (search.Entity.Percent>0 && skillcount >0)
            {
                totalSkillNeed = skillcount * 100 / search.Entity.Percent;
                realSkillNeed = skillcount * (((float)search.Entity.Percent) / 100);
                sqlWhere += " and skillCount <=" + totalSkillNeed;
            }
            if (!string.IsNullOrEmpty(search.Entity.City))
            {
                sqlWhere += string.Format(" and t.City ='{0}'",search.Entity.City);
            }
            if (search.Entity.Salary > 0)
            {
                sqlWhere += " and t.AvageMoney <=" + search.Entity.Salary;
            }
            if (!string.IsNullOrEmpty(studentTagStr))
            {
                List<string> realNeedTags = tags.Take((int)realSkillNeed).ToList();
                string realNeedSkillStr = string.Join(",",realNeedTags);
                sqlWhere += string.Format(" and t.Skills like '%{0}%' or t.Skills like '%{1}%'",studentTagStr,realNeedSkillStr);
            }
            //标签的匹配度 精确匹配度,判断匹配的数量
            sql = string.Format("{0}{1}",sql,sqlWhere);
            List<PositionExt> list = baseService.GetSqlPagedData<PositionExt, PositionMatchModel, int>(sql, parameter, out count);
            //foreach (PositionExt position in list)
            //{
            //    //查询对应职位的标签。
            //    position.Tags = this.GetTagsByPositionID(position.PositionID);
            //    if (position.Tags.Count > 0)
            //    {
            //        position.TagsJsonStr = JsonConvert.SerializeObject(position.Tags.Select(r => r.TagName).ToList());
            //    }
            //}
            return list;
        }


        public int UpdatePosition(Position position)
        {
            int result = -1;
            var match = context.Position.FirstOrDefault(r=>r.PositionID == position.PositionID);
            if (match != null)
            {
                match.PositionName = position.PositionName;
                match.PositionType = position.PositionType;
                match.PositionDesc = position.PositionDesc;
                context.Entry(match).State = System.Data.Entity.EntityState.Modified;
                result = context.SaveChanges();
            }
            return result;
        }
    }
}
