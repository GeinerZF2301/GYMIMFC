using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public partial class Empleado
    {
        
        public Empleado()
        {
            Cita = new HashSet<Cita>();
        }
            [Key]
         [Required(ErrorMessage = "Debe digitar el ID del Empleado")]
         [Display(Name = "Empleado ID:")]
         [MaxLength(50)]
         public string EmpleadoId { get; set; }
         [Required(ErrorMessage = "Debe digitar el nombre del Empleado")]
         [Display(Name = "Nombre:")]
         [StringLength(50)]
         public string Nombre { get; set; }

         [Required(ErrorMessage = "Debe digitar los apellidos del Empleado")]
         [Display(Name = "Apellidos:")]
         [StringLength(50)]
        public string Apellidos { get; set; }
         [Required(ErrorMessage = "Debe digitar la dirección del Empleado")]
         [StringLength(75)]
        public string Direccion { get; set; }
         [Required(ErrorMessage = "Debe digitar el # de teléfono del Empleado")]
         public string TelefonoFijo { get; set; }
         [Required(ErrorMessage = "Debe digitar el # de celular del Empleado")]
         [DataType(DataType.PhoneNumber)]
         public string TelefonoCelular { get; set; }
         public byte[] Foto { get; set; }
         public int ServicioId { get; set; }
         public virtual Servicios Servicios { get; set; }
         //public string msgError { get; set; }
         public virtual ICollection<Cita> Cita { get; set; }
     }
}

