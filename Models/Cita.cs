using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public partial class Cita
    {

        [Display(Name = "Cita ID")]
        [MaxLength(50)]
        public int citaId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Cliente ID")]
        public string clienteId { get; set; }
        [Display(Name = "ID Instructor")]
        [MaxLength(50)]
        public int? ServicioId { get; set; }
        [Display(Name = "ID Instructor")]
        [MaxLength(50)]
        public string EmpleadoId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor, inserte fecha")]
        public DateTime FechaCita { get; set; }

        [Display(Name = "ID Categoria Servicio")]
        [StringLength(50)]
        public string Descripcion { get; set; }
      
        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Servicios Servicios { get; set; } 
       
    }
}
