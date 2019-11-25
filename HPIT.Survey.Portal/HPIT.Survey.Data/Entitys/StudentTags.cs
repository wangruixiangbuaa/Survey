namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentTags
    {
        public int ID { get; set; }

        public int? StuNo { get; set; }

        public string TagID { get; set; }

        public int? SelfPoint { get; set; }

        public int? TeacherPoint { get; set; }

        public int? Status { get; set; }

        public virtual SkillTag SkillTag { get; set; }

        public virtual Student Student { get; set; }
    }
}
