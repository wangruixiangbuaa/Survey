namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menus()
        {
            RoleMenus = new HashSet<RoleMenus>();
        }

        [Key]
        public int MenuID { get; set; }

        [StringLength(20)]
        public string MenuName { get; set; }

        [StringLength(20)]
        public string MenuType { get; set; }

        public int? Status { get; set; }

        public int? ParentID { get; set; }

        public int? Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleMenus> RoleMenus { get; set; }
    }
}
