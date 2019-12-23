using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            string sql = @"SELECT p.*,c.CompanyName ,c.CompanyType 
                         FROM dbo.[Position] p 
                         left join dbo.[SurveyModel] s on p.SurveyID = s.SurveyID 
                         left join dbo.[Company] c on c.CompanyID = s.CompanyID";
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
    }
}
