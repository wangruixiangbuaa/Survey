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

        /// <summary>
        /// 行业分析
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public List<CommonStatistic> IndustryStatistic(string position)
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = string.Format(@"select  c.CompanyType name,count(c.CompanyType) value FROM [SurveyDB].[dbo].[SurveyModel] s 
                           left join [SurveyDB].[dbo].Company c on s.CompanyID = c.CompanyID where s.PositionName = '{0}' group by c.CompanyType",position);
            //string sql = @"select CompanyType name ,COUNT(CompanyType) value from [SurveyDB].[dbo].[Company] group by CompanyType";
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }

        public List<Company> IndustryStatisticDetail(string position, string type)
        {
            List<Company> Statistic = new List<Company>();
            string sql = string.Format(@"select s.StuName,s.Direction,s.Phone ,s.PositionName, c.* FROM [SurveyDB].[dbo].[SurveyModel] s left join [SurveyDB].[dbo].Company c
                                         on s.CompanyID = c.CompanyID
                                         where PositionName ='{0}' and c.CompanyType ='{1}'", position, type);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<Company>(sql).ToList();
            }
            return Statistic;
        }

        public List<GeneralSelectItem> GetAllPositionNames()
        {
            List<GeneralSelectItem> Statistic = new List<GeneralSelectItem>();
            string sql = string.Format(@" select distinct(s.PositionName) Text, s.PositionName Value FROM 
                           [SurveyDB].[dbo].[SurveyModel] s ");
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<GeneralSelectItem>(sql).ToList();
            }
            return Statistic;
        }



        public List<CommonStatistic> CityStatistic(string position)
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = string.Format(@"select  c.City name,count(c.City) value FROM [SurveyDB].[dbo].[SurveyModel] s 
                                         left join [SurveyDB].[dbo].Company c  on s.CompanyID = c.CompanyID 
                                         where s.PositionName = '{0}' group by c.City",position);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }


        public List<Company> CityStatisticDetail(string position, string city)
        {
            List<Company> Statistic = new List<Company>();
            string sql = string.Format(@"select s.StuName,s.Direction,s.Phone ,s.PositionName, c.* FROM [SurveyDB].[dbo].[SurveyModel] s left join [SurveyDB].[dbo].Company c
                                         on s.CompanyID = c.CompanyID
                                         where PositionName ='{0}' and c.City ='{1}'", position, city);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<Company>(sql).ToList();
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
