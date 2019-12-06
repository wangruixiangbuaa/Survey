using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.ExtEntitys
{
    public class MenuExt
    {
        public int MenuID { get; set; }

        [StringLength(20)]
        public string MenuName { get; set; }

        [StringLength(20)]
        public string MenuType { get; set; }

        public int? Status { get; set; }

        public int? ParentID { get; set; }

        public int? Sort { get; set; }

        public string MenuUrl { get; set; }

        public string MenuCode { get; set; }

        public int  roleID {get;set;}

        [NotMapped]
        public List<MenuExt> Children { get; set; } = new List<MenuExt>();
    }
}
