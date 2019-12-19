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

        public List<CommonStatistic> SalaryStatistic()
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            var salary5000 = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal < 5000).Count();
            var salary10000 = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal >= 5000 && r.WagesOfReal <= 10000).Count();
            var salary15000 = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal >= 10000 && r.WagesOfReal <= 15000).Count();
            var salary20000 = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal >= 15000 && r.WagesOfReal <= 20000).Count();
            Statistic.Add(new CommonStatistic() { name="低于5000", value = salary5000 });
            Statistic.Add(new CommonStatistic() { name = "大于5000低于10000", value = salary10000 });
            Statistic.Add(new CommonStatistic() { name = "大于10000低于15000", value = salary15000 });
            Statistic.Add(new CommonStatistic() { name = "大于15000低于20000", value = salary20000 });
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

        public List<CommonStatistic> JobStatistic()
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = @"select JobType name ,COUNT(JobType) value from [SurveyDB].[dbo].[ActiveJobs] group by JobType";
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }



    }
}
