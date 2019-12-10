using HPIT.Survey.Data.Entitys.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Adapter
{
    public class SurveyWorkFlow
    {
        public static SurveyWorkFlow Instance = new SurveyWorkFlow();
        private List<Activity> activities { get; set; }
        public SurveyWorkFlow()
        {
            this.activities = new List<Activity>();
            var pre = new Activity() { ActivityName = "学生", ActivitySort = 1, ActivityType = 1, RoleName = "学生" };
            pre.ActivityUsers.Add(new Entitys.Users() { UserName = "学生" });
            this.activities.Add(pre);
            var first = new Activity() { ActivityName = "人事经理审批", ActivitySort = 1, ActivityType = 1, RoleName = "人事经理" };
            first.ActivityUsers.Add(new Entitys.Users() { UserName = "人事经理" });
            this.activities.Add(first);
            var second = new Activity() { ActivityName = "教质经理审批", ActivitySort = 2, ActivityType = 1, RoleName = "教质经理" };
            second.ActivityUsers.Add(new Entitys.Users() { UserName = "陈文玉" });
            this.activities.Add(second);
            var third = new Activity() { ActivityName = "总经理审批", ActivitySort = 3, ActivityType = 1, RoleName = "总经理" };
            third.ActivityUsers.Add(new Entitys.Users() { UserName = "成先雄" });
            this.activities.Add(third);
            var final = new Activity() { ActivityName = "总经理完成审批", ActivitySort = 3, ActivityType = 1, RoleName = "审批完成" };
            final.ActivityUsers.Add(new Entitys.Users() { UserName = "已完成" });
            this.activities.Add(final);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string Pass(string userName, string roleName)
        {
            for (int i = 0; i < this.activities.Count; i++)
            {
                if (this.activities[i].RoleName == roleName)
                {
                    return this.activities[i + 1].ActivityUsers[0].UserName;
                }
            }
            return "";
        }

        public string Reject(string userName, string roleName)
        {
            for (int i = 0; i < this.activities.Count; i++)
            {
                if (this.activities[i].RoleName == roleName)
                {
                    return this.activities[i - 1].ActivityUsers[0].UserName;
                }
            }
            return "";
        }
    }
}
