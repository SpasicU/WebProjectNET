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
    
    public partial class Magacin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Magacin()
        {
            this.StavkaNarudzbenice = new HashSet<StavkaNarudzbenice>();
        }
    
        public int redBroj { get; set; }
        public int IDKnjige { get; set; }
        public int stanje { get; set; }
    
        public virtual Knjiga Knjiga { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StavkaNarudzbenice> StavkaNarudzbenice { get; set; }
    }
}
