using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPIT.Survey.Portal.Models
{
    public class StudentTagsModel
    {
        public List<StudentTags> tags { get; set; }

        public string stuNo { get; set; }

        public string positionName { get; set; }
    }
}