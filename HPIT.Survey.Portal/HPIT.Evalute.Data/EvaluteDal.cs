using HPIT.Data.Core;
using HPIT.Evalute.Data.Model;
using HPIT.StudentEvaluate.Core;
using System.Collections.Generic;
using System.Linq;

namespace HPIT.Evalute.Data
{
    public class EvaluteDal
    {
        public static EvaluteDal Instance = new EvaluteDal();
        public List<EvalStudent> GetMatchStudent(string name, string classNo)
        {
            string sql = @"select * from (
                           select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                           s.Name,s.Mobile,s.[Address],s.EducationBackground as GraduateSchool ,p.Name as pName,
                           (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                           (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                           p.Classroom,b.Name as bName from Student s 
                           left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                           left join Batch b on p.BatchId = b.Id
                           left join Major m on s.MajorId = m.Id 
                           ) t where Name=@Name or pName=@ClassName";
            EvalStudent stu = new EvalStudent();
            stu.Name = name;
            stu.ClassName = classNo;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            return result;
        }

        public List<EvalStudent> GetMatchStudentByNo(string StudentNo)
        {
            string sql = @"select * from (
                          select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                          s.Name,s.Mobile,s.[Address],s.GraduateSchool,p.Name as pName,
                          (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                          (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                          p.Classroom,b.Name as bName from Student s 
                          left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                          left join Batch b on p.BatchId = b.Id
                          left join Major m on s.MajorId = m.Id ) t where StudentNo=@StudentNo";
            EvalStudent stu = new EvalStudent();
            stu.StudentNo = StudentNo;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            return result;
        }


        public List<HPITMemberInfo> LoginMember(string loginName, string passWord)
        {
            string sql = @"select * from(  select m.RealName,m.Mobile,m.Email,r.Category,r.FullName,r.Description,r.OrganizeId 
                          ,o.FullName as oFullName,o.Address,o.Manager,o.Mobile as oMobile,ma.Password
                          from Member m 
                          left join RoleInfo r on m.RoleId = r.Id
                          left join Organize o on m.OrganizeId = o.Id
                          left join MemberAccount ma on ma.Id = m.MemberAccountId) t where t.RealName = @RealName and t.Password = @Password";
            HPITMemberInfo stu = new HPITMemberInfo();
            stu.RealName = loginName;
            stu.Password =Md5Encrypt.Md5(passWord);
            List<HPITMemberInfo> result = DapperDBHelper.Instance.ExcuteQuery<HPITMemberInfo>(sql, stu).ToList();
            return result;
        }
    }
}
