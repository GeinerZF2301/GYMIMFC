using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public partial class Empleado
    {
        [Key]

        [Required(ErrorMessage = "Debe digitar el nombre el nombre del empleado")]
        [Display(Name = "Nombre:")]
        public virtual string NombreEmpleado { get; set; }

        [Required(ErrorMessage = "Debe digitar el ID del empleado")]
        [Display(Name = "Empleado ID:")]
        public virtual short idEmpleado { get; set; }

        [Required(ErrorMessage = "Debe digitar el puesto del empleado")]
        [Display(Name = "Puesto:")]
        public virtual string Puesto { get; set; }

        [Required(ErrorMessage = "Debe digitar la zona del puesto del empleado")]
        [Display(Name = "Zona Puesto:")]
        public virtual string ZonaPuesto { get; set; }

        [Required(ErrorMessage = "Debe digitar la fecha de ingreso del empleado")]
        [Display(Name = "Fecha de ingreso:")]
        public virtual DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "Debe digitar el tiempo de contrato del empleado")]
        [Display(Name = "Fecha de ingreso:")]
        public virtual DateTime TiempoContrato { get; set; }

        [Required(ErrorMessage = "Debe digitar la cédula del empleado")]
        [Display(Name = "Cédula:")]
        public virtual string Cedula { get; set; }

        public virtual IList<Matricula> Matriculas { get; set; }
    }
}
