//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAIN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mcluster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mcluster()
        {
            this.t_initiative = new HashSet<t_initiative>();
        }
    
        public long id { get; set; }
        public long RegionID { get; set; }
        public long SubRegionID { get; set; }
        public Nullable<long> CountryID { get; set; }
        public string ClusterName { get; set; }
        public long InitYear { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_initiative> t_initiative { get; set; }
        public virtual mregion mregion { get; set; }
        public virtual msubregion msubregion { get; set; }
    }
}
