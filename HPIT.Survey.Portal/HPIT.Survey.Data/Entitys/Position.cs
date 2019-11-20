namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Position")]
    public partial class Position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Position()
        {
            SkillTags = new HashSet<SkillTags>();
        }

        public int PositionID { get; set; }

        public int? SurveyID { get; set; }

        [StringLength(20)]
        public string PositionName { get; set; }

        [StringLength(20)]
        public string PositionType { get; set; }

        [StringLength(2000)]
        public string PositionDesc { get; set; }

        public int? Status { get; set; }

        public int? CompanyID { get; set; }


        public string Source { get; set; }

        public string Years { get; set; }

        //public virtual SurveyModel SurveyModel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkillTags> SkillTags { get; set; }
    }
}
