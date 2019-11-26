using HPIT.Evalute.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HPIT.Data.Core;

namespace HPIT.Evalute.Data
{
    public class EvaluteDal
    {
        public static EvaluteDal Instance = new EvaluteDal();
        public List<EvalStudent> GetMatchStudent(string name,string classNo)
        {
            string sql = @"select * from (
select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
s.Name,s.Mobile,s.[Address],s.GraduateSchool,p.Name as pName,p.Classroom,b.Name as bName from Student s 
left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
left join Batch b on p.BatchId = b.Id
left join Major m on s.MajorId = m.Id 
) t where Name=@Name or pName=@ClassName";
            EvalStudent stu = new EvalStudent();
            stu.Name = name;
            stu.ClassName = classNo;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql,stu).ToList();
            return result;
        }

        public List<EvalStudent> GetMatchStudentByNo(string StudentNo)
        {
            string sql = @"select * from (
select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
s.Name,s.Mobile,s.[Address],s.GraduateSchool,p.Name as pName,p.Classroom,b.Name as bName from Student s 
left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
left join Batch b on p.BatchId = b.Id
left join Major m on s.MajorId = m.Id ) t where StudentNo=@StudentNo";
            EvalStudent stu = new EvalStudent();
            stu.StudentNo = StudentNo;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            return result;
        }
    }
}
