//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transportes_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Choferes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Choferes()
        {
            this.Rutas = new HashSet<Rutas>();
        }
    
        public int ID_Chofer { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Telefono { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Licencia { get; set; }
        public string UrlFoto { get; set; }
        public bool Disponibilidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rutas> Rutas { get; set; }
    }
}
