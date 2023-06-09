//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinema.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashReceipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashReceipt()
        {
            this.ProductCashReceipt = new HashSet<ProductCashReceipt>();
            this.Ticket = new HashSet<Ticket>();
        }
    
        public int IdCashReceipt { get; set; }
        public int IdUser { get; set; }
        public int IdEmployee { get; set; }
        public System.DateTime DateSale { get; set; }
        public Nullable<decimal> FullCost { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCashReceipt> ProductCashReceipt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
