using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
namespace HPIT.Survey.Data.Entitys
{
    [Table("StudentEvaluate")]
    public class StudentEval
    {
        public StudentEval()
        {
        }

        [Key]
        public string StudentNo { get; set; }

        public string StudentName { get; set; }

        public int? Score { get; set; }

        public string Direction { get; set; }

        public int? TeacherPoint { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
