using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class StudentDal
    {
        public static StudentDal Instance = new StudentDal();

        public SurveyContext context { get; set; }
        public StudentDal()
        {
            this.context = new SurveyContext();
        }

        /// <summary>
        /// 添加学生标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public int AddStudentTags(string stuNo,List<StudentTags> tags)
        {
            using (var db = new SurveyContext())
            {
                 db.Database.ExecuteSqlCommand(
                   string.Format("delete from dbo.StudentTags where StudentNo='{0}'", stuNo));
            }
            context.StudentTags.AddRange(tags);
            return context.SaveChanges();
        }
    }
}
