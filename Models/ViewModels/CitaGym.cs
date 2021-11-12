using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models.ViewModels
{
    public class CitaGym
    {
        [Display(Name = "Cita ID")]

        public int CitaId { get; set; }
        [Display(Name = "Cliente ID")]
        public string ClienteId { get; set; }
        [Display(Name = "Cliente")]
        public string NombreCliente { get; set; }
        public string EmpleadoId { get; set; }
        [Display(Name = "Empleado")]
        public string NombreEmpleado { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha cita")]
        public DateTime FechaCita { get; set; }
        public string Comentarios { get; set; }
        public int ServicioId { get; set; }
        [Display(Name = "Servicio")]
        public string NombreServicio { get; set; }
        public int SelectedOption { get; set; }
        public IEnumerable<SelectListItem> Lista { get; set; }
    }
}
    

