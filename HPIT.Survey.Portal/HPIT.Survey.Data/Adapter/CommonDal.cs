using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class CommonDal
    {
        public static CommonDal Instance = new CommonDal();
        public List<GeneralSelectItem> GetYears()
        {
            List<GeneralSelectItem> items = new List<GeneralSelectItem>();
            int start = 2010;
            for (int i = start; i < 2020; i++)
            {
                items.Add(new GeneralSelectItem() { Text = i.ToString(),Value = i.ToString() });
            }
            return items;
        }


        public List<CommonStatistic> IndustryStatistic()
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = @"select CompanyType name ,COUNT(CompanyType) value from [SurveyDB].[dbo].[Company] group by CompanyType";
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }

        public List<GeneralSelectItem> GetCount()
        {
            List<GeneralSelectItem> items = new List<GeneralSelectItem>();
            int start = 1;
            for (int i = start; i < 20; i++)
            {
                items.Add(new GeneralSelectItem() { Text = i.ToString(), Value = i.ToString() });
            }
            return items;
        }



    }
}
