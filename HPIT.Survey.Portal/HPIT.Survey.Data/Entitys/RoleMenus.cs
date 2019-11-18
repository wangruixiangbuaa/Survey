namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoleMenus
    {
        public int ID { get; set; }

        public int? MenuID { get; set; }

        public int? RoleID { get; set; }

        public virtual Menus Menus { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
