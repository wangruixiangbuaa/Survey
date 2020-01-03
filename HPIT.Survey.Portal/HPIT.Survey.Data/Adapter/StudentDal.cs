using HPIT.Data.Core;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public int AddStudentTags(string stuNo,List<StudentTags> tags,StudentEval eval)
        {
            //添加一次评分
            var match = context.StudentEvaluate.FirstOrDefault(r => r.StudentNo == stuNo);
            if (match == null)
            {
                context.StudentEvaluate.Add(eval);
            }
            else
            {
                using (var db = new SurveyContext())
                {
                    db.Database.ExecuteSqlCommand(
                       string.Format("update dbo.StudentEvaluate set Score={0}, Direction = '{1}' where StudentNo='{2}'", eval.Score,eval.Direction, stuNo));
                }
            }
            using (var db = new SurveyContext())
            {
                 db.Database.ExecuteSqlCommand(
                   string.Format("delete from dbo.StudentTags where StudentNo='{0}'", stuNo));
            }
            context.StudentTags.AddRange(tags);
            return context.SaveChanges();
        }

        /// <summary>
        /// 更新老师的评分
        /// </summary>
        /// <param name="stuNo"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public int UpdateStudentTags(string stuNo, List<StudentTags> tags)
        {
            int result = 0;
            using (var db = new SurveyContext())
            {
                result = db.Database.ExecuteSqlCommand(
                    string.Format("update dbo.StudentTags set TeacherPoint = 0 where StudentNo='{0}'", stuNo));
                foreach (var item in tags)
                {
                    result= db.Database.ExecuteSqlCommand(
                    string.Format("update dbo.StudentTags set TeacherPoint = {0} where TagID='{1}' and StudentNo ='{2}'", item.TeacherPoint ,item.TagID,stuNo));
                }
                int sum = (int)tags.Sum(r => r.TeacherPoint);
                result = db.Database.ExecuteSqlCommand(
                string.Format("update dbo.StudentEvaluate set TeacherPoint = {0} where StudentNo='{1}'", sum, stuNo));
            }
            return result;
        }

        public List<StudentEval> GetPageData(SearchModel<StudentEval> search, out int count)
        {
            GetPageListParameter<StudentEval, string> parameter = new GetPageListParameter<StudentEval, string>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.StudentNo;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => !string.IsNullOrEmpty(t.StudentNo);
            //查询数据
            if (!string.IsNullOrEmpty(search.Entity.PEM))
            {
                Expression<Func<StudentEval, bool>> Where = item => item.PEM == search.Entity.PEM;
                parameter.whereLambda = ExpressionExt.ReBuildExpression<StudentEval>(parameter.whereLambda, Where);
            }

            if (!string.IsNullOrEmpty(search.Entity.PRM))
            {
                Expression<Func<StudentEval, bool>> Where = item => item.PRM == search.Entity.PRM;
                parameter.whereLambda = ExpressionExt.ReBuildExpression<StudentEval>(parameter.whereLambda, Where);
            }
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<StudentEval> list = baseService.GetSimplePagedData<StudentEval, string>(parameter, out count);
            return list;
        }


    }
}
