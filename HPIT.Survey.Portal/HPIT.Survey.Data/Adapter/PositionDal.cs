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
