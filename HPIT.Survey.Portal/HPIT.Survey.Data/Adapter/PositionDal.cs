using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
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
            return list;
        }
    }
}
