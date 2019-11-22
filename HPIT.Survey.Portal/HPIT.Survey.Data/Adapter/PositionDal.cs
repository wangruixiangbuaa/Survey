using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public object GetPageData(SearchModel<Position> search, out int count)
        {
            GetPageListParameter<Position, int> parameter = new GetPageListParameter<Position, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.PositionID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.PositionID > 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<Position> list = baseService.GetSimplePagedData<Position, int>(parameter, out count);
            return list;
        }
    }
}
