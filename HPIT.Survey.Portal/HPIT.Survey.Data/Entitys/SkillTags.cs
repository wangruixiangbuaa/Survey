namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SkillTags
    {
        public int ID { get; set; }

        public int? TagID { get; set; }

        public int? PositionID { get; set; }

        public virtual Position Position { get; set; }

        public virtual SkillTag SkillTag { get; set; }
    }
}
