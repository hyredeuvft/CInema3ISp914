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
    
    public partial class NetCinema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NetCinema()
        {
            this.MovieHall = new HashSet<MovieHall>();
            this.Film = new HashSet<Film>();
        }
    
        public int IdCinema { get; set; }
        public string CinemaTitle { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CinemaEmail { get; set; }
        public int IdEmployee { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovieHall> MovieHall { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Film> Film { get; set; }
    }
}