using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Camiones_DTO
    {
        //DTO => Data Transfer Object
        //
        //
        [Key] //data annotation
        public int ID_Camion { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Matrícula")] //DataHelper
        public string Matricula { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Tipo_Camion")] //DataHelper
        public string Tipo_Camion { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Marca")] //DataHelper
        public string Marca { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Modelo")] //DataHelper
        public string Modelo { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Capacidad")] //DataHelper
        public int Capacidad { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Kilometraje")] //DataHelper
        public double Kilometraje { get; set; }

        [DataType(DataType.ImageUrl)] //DataHelper
        public string UrlFoto { get; set; }
        [Required] //DAta Annotation
        [Display(Name = "Disponibilidad")] //DataHelper
        public bool Disponibilidad { get; set; }
    }
}
