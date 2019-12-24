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

        public List<GeneralSelectItem> GetAllPositionTbNames()
        {
            List<GeneralSelectItem> Statistic = new List<GeneralSelectItem>();
            string sql = string.Format(@" select distinct(s.PositionName) Text, s.PositionName Value FROM 
                           [SurveyDB].[dbo].[Position] s ");
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


        public List<CommonStatistic> SalaryStatistic(string position)
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();

            var salary1 = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal < 2500 && r.PositionName == position).Count();
            Statistic.Add(new CommonStatistic() { name = "0~2500", value = salary1 });
            //2500-3000 3000-3500
            //3500 -4000 4000-4500 
            //4500 -5000 5000-5500 
            //5500 -6000 6000-6500 
            //6500 -7000 7000-7500 
            //7500 -8000 8000-8500
            //8500-9000 9000-9500
            //9500-10000 10500-11000
            //11000-11500 11500-12000
            int start = 0;
            int end = 0;
            for (int i = 0; i < 18; i++)
            {
                start = 2500 + i * 500;
                end = 3000 + i * 500;
                var salaryCount = SurveyContext.Instance.SurveyModel.Where(r => r.WagesOfReal >= start && r.WagesOfReal < end && r.PositionName == position).Count();
                if (salaryCount > 0)
                {
                    Statistic.Add(new CommonStatistic() { name = string.Format(start + "~" + end), value = salaryCount });
                }
            }
            return Statistic;
        }


        public List<Company> SalaryStatisticDetail(string position,string range)
        {
            int begin = Convert.ToInt32(range.Split('~')[0]);
            int end = Convert.ToInt32(range.Split('~')[1]);
            List<Company> matchs = new List<Company>();
            string sql = string.Format(@"select s.PositionName,s.PositionName,s.WagesOfReal,c.* from dbo.SurveyModel s 
                           left join dbo.Company c on s.CompanyID = c.CompanyID where s.PositionName = '{0}' and s.WagesOfReal >= {1} and s.WagesOfReal < {2} ", position,begin,end);
            using (var context = new SurveyContext())
            {
                matchs = context.Database.SqlQuery<Company>(sql).ToList();
            }
            return matchs;
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



        //技术点分析
        public List<CommonStatistic>  SkillStatistic(string position)
        {
            List<CommonStatistic> Statistic = new List<CommonStatistic>();
            string sql = string.Format(@"select t.TagName name ,count(t.TagName) value from  dbo.SkillTags ks 
                                         left join dbo.Position p on ks.PositionID = p.PositionID
                                         left join dbo.SkillTag t on ks.TagID = t.TagID  
                                         where p.PositionName = '{0}' group by t.TagName ", position);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<CommonStatistic>(sql).ToList();
            }
            return Statistic;
        }


        public List<Company> SkillStatisticDetail(string position, string tagName)
        {
            List<Company> Statistic = new List<Company>();
            string sql = string.Format(@"select distinct(c.CompanyName),c.CompanyType,c.CompanyDetailType,
                                         c.City,c.CompanyID,c.CompanyNo,c.CompanyDesc,c.Status,c.DepartName from  dbo.SkillTags ks 
                                         left join dbo.Position p on ks.PositionID = p.PositionID
                                         left join dbo.SkillTag t on ks.TagID = t.TagID
                                         left join dbo.SurveyModel s on s.SurveyID = p.SurveyID
                                         left join dbo.Company c on s.CompanyID = c.CompanyID
                                         where p.PositionName = '{0}'  and t.TagName = '{1}'", position, tagName);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<Company>(sql).ToList();
            }
            return Statistic;
        }


        public int AddCompany(Company company)
        {
            var result = 0;
            using (var context = new SurveyContext())
            {
                context.Company.Add(company);
                result = context.SaveChanges();
            }
            return result;
        }

        public int UpdateCompany(Company company)
        {
            var result = 0;
            using (var context = new SurveyContext())
            {
                var match = context.Company.FirstOrDefault(c => c.CompanyID == company.CompanyID);
                if (match != null)
                {
                    context.Entry(match).State = System.Data.Entity.EntityState.Modified;
                    result = context.SaveChanges();
                }
            }
            return result;
        }

    }
}
