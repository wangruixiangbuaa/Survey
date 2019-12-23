using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class ProjectDal
    {
        public static ProjectDal Instance = new ProjectDal();

        public SurveyContext context { get; set; }
        public ProjectDal()
        {
            this.context = new SurveyContext();
        }

        public List<SkillTagStatic> ProjectStatistic(string position)
        {
            List<SkillTagStatic> skillStatistic = new List<SkillTagStatic>();
            string sql = string.Format(@"select p.ProjectType name ,COUNT(*) value  FROM [SurveyDB].[dbo].Project p 
                                         left join [SurveyDB].[dbo].[SurveyModel] s  on s.SurveyID = p.SurveyID where s.PositionName='{0}' group by p.ProjectType", position);
            //string sql = @"select  ProjectType name ,COUNT(ProjectType) value from [SurveyDB].[dbo].[Project] group by ProjectType";
            using (var context = new SurveyContext())
            {
                skillStatistic = context.Database.SqlQuery<SkillTagStatic>(sql).ToList();
            }
            return skillStatistic;
        }


        public List<ProjectExt> GetProjectListByType(string position, string type)
        {
            List<ProjectExt> Statistic = new List<ProjectExt>();
            string sql = string.Format(@" select s.StuName,s.Direction,s.Phone,s.PositionName,p.*,c.CompanyName,c.CompanyType FROM [SurveyDB].[dbo].Project p 
                                          left join [SurveyDB].[dbo].[SurveyModel] s  on s.SurveyID = p.SurveyID
                                          left join  [SurveyDB].[dbo].Company c on s.CompanyID = c.CompanyID 
                                          where PositionName='{0}' and p.ProjectType='{1}'", position, type);
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<ProjectExt>(sql).ToList();
            }
            return Statistic;
        }


        public List<GeneralSelectItem> GetAllPositionNames()
        {
            List<GeneralSelectItem> Statistic = new List<GeneralSelectItem>();
            string sql = string.Format(@" select distinct(s.PositionName) Text, s.PositionName Value FROM [SurveyDB].[dbo].Project p 
                         left join [SurveyDB].[dbo].[SurveyModel] s  on s.SurveyID = p.SurveyID");
            using (var context = new SurveyContext())
            {
                Statistic = context.Database.SqlQuery<GeneralSelectItem>(sql).ToList();
            }
            return Statistic;
        }
    }
}
