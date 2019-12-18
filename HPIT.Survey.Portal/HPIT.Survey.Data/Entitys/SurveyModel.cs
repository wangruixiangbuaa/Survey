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
            //现在招聘的岗位
            ActiveJobs = new HashSet<ActiveJobs>();
            //公司存在的岗位信息
            Position = new HashSet<Position>();
            //项目信息
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

        public string StudentNo { get; set; }

        [StringLength(64)]
        public string CompanyNo { get; set; }

        public decimal? WagesOfPeriod { get; set; }

        public decimal? WagesOfFull { get; set; }

        public decimal? WagesOfReal { get; set; }

        public int? Status { get; set; }

        public int? Type { get; set; }

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

        public string PEM { get; set; }

        public string PRM { get; set; }

        public string City { get; set; }

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

        public virtual Student Student { get; set; } 

        [NotMapped]
        public string CurrentRoleName { get; set; }
    }
}
