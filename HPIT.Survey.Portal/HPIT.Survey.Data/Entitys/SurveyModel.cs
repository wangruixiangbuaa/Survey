namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SurveyModel")]
    public partial class SurveyModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SurveyModel()
        {
            ActiveJobs = new HashSet<ActiveJobs>();
            Position = new HashSet<Position>();
            Project = new HashSet<Project>();
        }

        [Key]
        public int SurveyID { get; set; }

        [StringLength(50)]
        public string SurveyTickNumber { get; set; }

        public int? StuNo { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(10)]
        public string StuName { get; set; }

        [StringLength(64)]
        public string CompanyNo { get; set; }

        public decimal? WagesOfPeriod { get; set; }

        public decimal? WagesOfFull { get; set; }

        public decimal? WagesOfReal { get; set; }

        public int? Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Year { get; set; }

        public string Batch { get; set; }

        public string Direction { get; set; }

        public string ProjectName { get; set; }

        public string Phone { get; set; }


        public string AuditName { get; set; }

        public int? AuditStatus { get; set; }

        public string School { get; set; }

        public string DepartName { get; set; }

        public string PositionName { get; set; }

        public string CorworkPhone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActiveJobs> ActiveJobs { get; set; }

        public virtual Company Company { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Position> Position { get; set; }

        [NotMapped]
        public Position CurrentPosition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }

        [NotMapped]
        public Project CurrentProject { get; set; }

        public virtual Student Student { get; set; } = new Student();
    }
}
