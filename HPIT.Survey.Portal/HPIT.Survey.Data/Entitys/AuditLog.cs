using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        public int ID { get; set; }

        public string AuditName { get; set; }

        public DateTime AuditTime { get; set; }

        public int SurveyID { get; set; }

        public int AuditState { get; set; }

        public string RoleName { get; set; }

        public int UserID { get; set; }
    }
}
