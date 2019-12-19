namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            //SurveyModel = new HashSet<SurveyModel>();
        }

        public int CompanyID { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string CompanyType { get; set; }

        [StringLength(20)]
        public string CompanyDetailType { get; set; }

        [StringLength(4000)]
        public string CompanyDesc { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string DepartName { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        [StringLength(50)]
        public string CompanyNo { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SurveyModel> SurveyModel { get; set; }
    }
}
