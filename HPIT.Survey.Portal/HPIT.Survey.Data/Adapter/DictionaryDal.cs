using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class DictionaryDal
    {
        public static DictionaryDal Instance = new DictionaryDal();

        public SurveyContext context { get; set; }

        public DictionaryDal()
        {
            this.context = new SurveyContext();
        }

        /// <summary>
        /// 分页查询的方法
        /// </summary>
        /// <param name="search"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public object GetPageData(SearchModel<Dictionary> search, out int count)
        {
            GetPageListParameter<Dictionary, int> parameter = new GetPageListParameter<Dictionary, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.ID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.ID != 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<Dictionary> list = baseService.GetSimplePagedData<Dictionary, int>(parameter, out count);
            return list;
        }
    }
}
