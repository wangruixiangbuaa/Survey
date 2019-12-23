using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.ExtEntitys
{
    public class ProjectExt
    {
        public int ProjectID { get; set; }

        public int? SurveyID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectType { get; set; }

        public int? ProjectTypeID { get; set; }

        public string ProjectDesc { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string CompanyDetailType { get; set; }



    }
}
