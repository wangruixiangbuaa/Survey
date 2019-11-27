using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.ExtEntitys
{
    public  class PositionExt
    {
        public string PositionID { get; set; }

        public int? SurveyID { get; set; }

        public string PositionName { get; set; }

        public string PositionType { get; set; }

        public string PositionDesc { get; set; }

        public int? Status { get; set; }

        public int? CompanyID { get; set; }

        public string Source { get; set; }

        public string Years { get; set; }
        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        [NotMapped]
        public List<SkillTag> Tags { get; set; }

        [NotMapped]
        public string TagsJsonStr { get; set; }
    }
}
