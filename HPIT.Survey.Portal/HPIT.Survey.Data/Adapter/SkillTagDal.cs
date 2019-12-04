using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class SkillTagDal
    {
        public static SkillTagDal Instance = new SkillTagDal();

        public SurveyContext context { get; set; }
        public SkillTagDal()
        {
            this.context = new SurveyContext();
        }

        public object GetPageData(SearchModel<SkillTag> search, out int count)
        {
            GetPageListParameter<SkillTag, string> parameter = new GetPageListParameter<SkillTag, string>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.TagID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.TagID != "";
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<SkillTag> list = baseService.GetSimplePagedData<SkillTag, string>(parameter, out count);
            return list;
        }


        public int UpdateTag(SkillTag tag)
        {
            tag.Creatime = DateTime.Now;
            context.Entry(tag).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public List<SkillTag> DirectionTags(string direction)
        {
            return context.SkillTag.Where(r => r.Direction == direction).ToList();
        }

        public List<SkillTagStatic> TagStatistic(string direction)
        {
            List<SkillTagStatic> skillStatistic = new List<SkillTagStatic>();
            string sql = string.Format(@"select t.TagName as name,count(t.TagName) as value from (
                                         SELECT ts.*,tg.TagName,tg.Direction,tg.CourseName,tg.TagType
                                         FROM [SurveyDB].[dbo].[SkillTags] ts 
                                         left join [SurveyDB].[dbo].[SkillTag] tg on ts.TagID = tg.TagID where tg.Direction='{0}') t group by t.TagName",direction);
            using (var context = new SurveyContext())
            {
                skillStatistic = context.Database.SqlQuery<SkillTagStatic>(sql).ToList();
            }
            return skillStatistic;
        }
        /// <summary>
        /// 添加标签的方法
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public int AddTags(List<SkillTag> tags)
        {
            var result = 0;
            foreach (var tag in tags)
            {
                SkillTag match = this.context.SkillTag.Where(r => r.TagName == tag.TagName).FirstOrDefault();
                if (match == null)
                {
                    tag.TagID = Guid.NewGuid().ToString();
                    this.context.SkillTag.Add(tag);
                    this.context.SkillTags.Add(new SkillTags() { PositionID = tag.PositionID, TagID = tag.TagID.ToString(), ID = Guid.NewGuid().ToString() });
                }
                else
                {
                    SkillTags tagMatch = this.context.SkillTags.Where(r => r.TagID == match.TagID && r.PositionID ==tag.PositionID).FirstOrDefault();
                    if (tagMatch == null)
                    {
                        this.context.SkillTags.Add(new SkillTags() { PositionID = tag.PositionID, TagID = match.TagID, ID = Guid.NewGuid().ToString() });
                    }
                }
            }
            result = this.context.SaveChanges();
            return result;
        }
    }
}
