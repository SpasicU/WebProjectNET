//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Knjizara.Models.EFRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Knjiga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Knjiga()
        {
            this.Magacin = new HashSet<Magacin>();
            this.StavkaRacuna = new HashSet<StavkaRacuna>();
        }
    
        public int IDKnjige { get; set; }
        public string autor { get; set; }
        public string naziv { get; set; }
        public int tiraz { get; set; }
        public string izdavastvo { get; set; }
        public int godinaIzdanja { get; set; }
        public float jedCena { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Magacin> Magacin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StavkaRacuna> StavkaRacuna { get; set; }
    }
}