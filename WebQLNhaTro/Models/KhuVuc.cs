//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebQLNhaTro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhuVuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuVuc()
        {
            this.NhaTroes = new HashSet<NhaTro>();
        }
    
        public int MaKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public System.DateTime NgayDang { get; set; }
        public System.DateTime NgaySua { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhaTro> NhaTroes { get; set; }
    }
}
