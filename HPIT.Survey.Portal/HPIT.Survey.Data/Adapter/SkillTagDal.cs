using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
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

        public int AddTags(List<SkillTag> tags)
        {
            var result = 0;
            foreach (var tag in tags)
            {
                var match = this.context.SkillTag.Where(r => r.TagName == tag.TagName).FirstOrDefault();
                if (match == null)
                {
                    tag.TagID = Guid.NewGuid().ToString();
                    this.context.SkillTag.Add(tag);
                    this.context.SkillTags.Add(new SkillTags() { PositionID = tag.PositionID, TagID = tag.TagID.ToString(), ID = Guid.NewGuid().ToString() });
                }
                else
                {
                    this.context.SkillTags.Add(new SkillTags() { PositionID = tag.PositionID, TagID = match.TagID.ToString(), ID = Guid.NewGuid().ToString() });
                }
                result = this.context.SaveChanges();
            }
            return result;
        }
    }
}
