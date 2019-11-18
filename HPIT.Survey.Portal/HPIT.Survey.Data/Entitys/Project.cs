namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public int ProjectID { get; set; }

        public int? SurveyID { get; set; }

        [StringLength(100)]
        public string ProjectName { get; set; }

        [StringLength(10)]
        public string ProjectType { get; set; }

        public int? ProjectTypeID { get; set; }

        [StringLength(3000)]
        public string ProjectDesc { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        //public virtual SurveyModel SurveyModel { get; set; }
    }
}
