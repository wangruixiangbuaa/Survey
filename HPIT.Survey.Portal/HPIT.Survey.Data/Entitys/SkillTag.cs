namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SkillTag")]
    public partial class SkillTag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SkillTag()
        {
            SkillTags = new HashSet<SkillTags>();
            //StudentTags = new HashSet<StudentTags>();
        }

        [Key]
        public string TagID { get; set; }

        [StringLength(20)]
        public string TagName { get; set; }

        [StringLength(20)]
        public string TagType { get; set; }

        [StringLength(20)]
        public string CourseName { get; set; }

        public int? CourseID { get; set; }

        [StringLength(20)]
        public string Direction { get; set; }

        public int? DirectionID { get; set; }

        public DateTime? Creatime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkillTags> SkillTags { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentTags> StudentTags { get; set; }

        [NotMapped]
        public string PositionID { get; set; }
    }
}
