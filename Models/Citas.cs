using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public class Citas{

        [Display(Name = "Cita ID")]

        public int CitaId { get; set; }
        [Display(Name = "Cliente ID")]
        public string PacienteId { get; set; }
        [Display(Name = "Cliente")]
        public string Nombre { get; set; }
        public string idEmpleado { get; set; }
        [Display(Name = "Instructor")]
        public string NombreEmpleado { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
       // [Display(Name = "Fecha cita")]
        public DateTime FechaCita { get; set; }
        public string idCategoria { get; set; }
        public int idServicio { get; set; }
        [Display(Name = "Servicio")]
        public string NombreServicio { get; set; }
        public int SelectedOption { get; set; }
        public IEnumerable<SelectListItem> Lista { get; set; }
    }
}

