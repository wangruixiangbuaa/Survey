using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
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
            GetPageListParameter<SkillTag, int> parameter = new GetPageListParameter<SkillTag, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.TagID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.TagID > 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<SkillTag> list = baseService.GetSimplePagedData<SkillTag, int>(parameter, out count);
            return list;
        }
    }
}
