using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            //Cita = new HashSet<Cita>();
        }
        [Required(ErrorMessage = "Digite el ID del Cliente")]
        [Display(Name = "ID")]
        public string ClienteId { get; set; }
        [Required(ErrorMessage = "Digite el Nombre del Cliente")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Digite los Apellidos del Cliente")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Digite la Dirección del Cliente")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Digite el número Telefónico del Cliente")]
        [Display(Name = "Teléfono")]
        public string TelefonoContacto { get; set; }
        [Required(ErrorMessage = "Digite el Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",
            ErrorMessage = "Ingrese una dirección de correo válida")]
        public string Email { get; set; }
        public bool? Foto { get; set; }
        // public virtual ICollection<Cita> Cita { get; set; }
    }
}