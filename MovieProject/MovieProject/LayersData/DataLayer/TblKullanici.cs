//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblKullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblKullanici()
        {
            this.TblKisiGezinti = new HashSet<TblKisiGezinti>();
        }
    
        public int KullaniciId { get; set; }
        public int KisiId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public Nullable<System.DateTime> SonGiris { get; set; }
        public Nullable<System.DateTime> KayitTarihi { get; set; }
        public Nullable<bool> Aktif { get; set; }
    
        public virtual TblKisi TblKisi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblKisiGezinti> TblKisiGezinti { get; set; }
    }
}
