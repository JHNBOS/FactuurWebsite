//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Factuur
{
    using System;
    using System.Collections.Generic;
    
    public partial class debiteuren
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public debiteuren()
        {
            this.facturen = new HashSet<facturen>();
        }
    
        public int ID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public string Land { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<facturen> facturen { get; set; }
    }
}
