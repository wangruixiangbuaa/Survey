using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Entitys
{
    public class Enumerations
    {
        public enum Day
        {
            [Display(Name = "星期天")]
            Sunday,
            [Display(Name = "星期一")]
            Monday,
            [Display(Name = "星期二")]
            Tuesday,
            [Display(Name = "星期三")]
            Wednesday,
            [Display(Name = "星期四")]
            Thursday,
            [Display(Name = "星期五")]
            Friday,
            [Display(Name = "星期六")]
            Saturday
        }

        public enum SurveyStatus
        {
            [Display(Name = "草稿")]
            draft,
            [Display(Name = "审核中")]
            audit,
            [Display(Name = "通过")]
            pass,
            [Display(Name = "完成")]
            complete,
            [Display(Name = "未通过")]
            reject,
            [Display(Name = "作废")]
            cancel,

        }

        public enum DataStatus
        {
            [Display(Name = "启用")]
            use,
            [Display(Name = "删除")]
            delete
        }

        public enum SurveyType
        {
            [Display(Name = "已就业学生")]
            User,
            [Display(Name = "未就业学生")]
            Student
        }
    }
}
