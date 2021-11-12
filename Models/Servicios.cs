using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace GYMIMFC.Models
{
    public partial class Servicios
    {
        public Servicios()
        {
            Empleado = new HashSet<Empleado>();
        }
            
        [Key]
        [DisplayName("ID")]
        [Required(ErrorMessage = "Debe digitar el ID de la ënfermedad")]
        public int ServicioId { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe digitar el nombre de la enfermedad")]
        public string Nombre { get; set; }
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Debe digitar la descripción de la ënfermedad")]
        public string Descripcion { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
   
