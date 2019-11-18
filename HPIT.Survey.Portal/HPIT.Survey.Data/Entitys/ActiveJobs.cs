namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActiveJobs
    {
        [Key]
        public int JobID { get; set; }

        [StringLength(50)]
        public string JobName { get; set; }

        [StringLength(20)]
        public string JobType { get; set; }

        [StringLength(2000)]
        public string JobDesc { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? SurveyID { get; set; }

        public int? PositionID { get; set; }

        //public virtual SurveyModel SurveyModel { get; set; }
    }
}
