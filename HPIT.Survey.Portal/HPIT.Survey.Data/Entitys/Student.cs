namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            StudentTags = new HashSet<StudentTags>();
            //SurveyModel = new HashSet<SurveyModel>();
        }

        [Key]
        public int? StuNo { get; set; }

        public string StudentNo { get; set; }

        [StringLength(50)]
        public string StuName { get; set; }

        [StringLength(50)]
        public string ClassNo { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public decimal? WagesOfPeriod { get; set; }

        public decimal? WagesOfFull { get; set; }

        public decimal? WagesOfReal { get; set; }

        public int? Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? AlterTime { get; set; }

        [StringLength(50)]
        public string CurrentCompanyNo { get; set; }

        public int? Year { get; set; }

        [StringLength(20)]
        public string Batch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTags> StudentTags { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SurveyModel> SurveyModel { get; set; }
    }
}
