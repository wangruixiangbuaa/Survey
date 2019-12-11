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

        public List<SkillTagStatic> ProjectStatistic()
        {
            List<SkillTagStatic> skillStatistic = new List<SkillTagStatic>();
            string sql = @"select  ProjectType name ,COUNT(ProjectType) value from [SurveyDB].[dbo].[Project] group by ProjectType";
            using (var context = new SurveyContext())
            {
                skillStatistic = context.Database.SqlQuery<SkillTagStatic>(sql).ToList();
            }
            return skillStatistic;
        }
    }
}
