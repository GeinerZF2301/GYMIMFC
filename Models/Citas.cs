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
        public int clienteId { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Debe digitar el nombre del Cliente")]
        [StringLength(50)]
        public string nombreCliente { get; set; }

        [Display(Name = "ID Instructor")]
        [MaxLength(50)]
        public int idEmpleado { get; set; }

        [Display(Name = "Instructor")]
        [Required(ErrorMessage = "Debe digitar el nombre del Instructor")]
        [StringLength(50)]
        public string nombreEmpleado { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor, inserte fecha")]
        public DateTime FechaCita { get; set; }

        [Display(Name = "ID Categoria Servicio")]
        [MaxLength(50)]
        public int idCategoria { get; set; }
        [Display(Name = "ID Servicio")]
        [MaxLength(50)]
        public int idServicio { get; set; }
        [Display(Name = "Servicio")]
        [Required(ErrorMessage = "Debe seleccionar el nombre del Servicio")]
        public string nombreServicio { get; set; }
        public int SelectedOption { get; set; }
        public IEnumerable<SelectListItem> Lista { get; set; }
    }
}
