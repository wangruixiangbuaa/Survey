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

        public int DeleteDic(int id)
        {
            int result = 0;
            using (var db = new SurveyContext())
            {
                result = db.Database.ExecuteSqlCommand(
                  string.Format("delete from dbo.Dictionary where ID={0}", id));
            }
            return result;
        }

        public int UpdateDic(Dictionary dictionary)
        {
            Dictionary match = this.context.Dictionary.FirstOrDefault(r=>r.ID==dictionary.ID);
            int result = 0;
            if (match != null)
            {
                this.context.Entry(match).State = System.Data.Entity.EntityState.Modified;
                result = this.context.SaveChanges();
            }else
            {
                this.context.Dictionary.Add(dictionary);
            }
            return result;
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
