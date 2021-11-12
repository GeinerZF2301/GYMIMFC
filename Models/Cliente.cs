using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{ 
  public class Cliente
    {
    [Key]
    [Display(Name = "ID Ciente ")]
    [MaxLength(100)]
    public string ClienteId { get; set; }
    [Display(Name = "Nombre del Cliente")]
    [StringLength(50)]
    [Required(ErrorMessage = "Debe ingresar el nombre del cliente")]
    public string nombreCliente { get; set; }
    [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor incluya los apellidos del paciente")]
        public string Apellidos { get; set; } 
    public virtual DateTime fechaNacimiento { get; set; }
    [Display(Name = "Registre su peso")]
    [Required(ErrorMessage = "Debe registrar su peso")]

    public double Peso { get; set; }
    [Display(Name = "Registre su IMC")]
    [Required(ErrorMessage = "Debe registrar su IMC")]
    public double IMC { get; set; }
    [Display(Name = "Registre su altura")]
    [Required(ErrorMessage = "Debe registrar su altura")]
    public double Altura { get; set; }
    [Display(Name = "Edad")]
    [Required(ErrorMessage = "Debe registrar su edad")]
    [Range(0,99)]
    public int Edad { get; set; }
    [Display(Name = "Direccion")]
    [Required(ErrorMessage = "Por favor incluya la dirección del paciente")]
    public string Direccion { get; set; }
    [Display(Name = "Cédula del Cliente")]
    [Required(ErrorMessage = "Debe registrar su cédula")]
    public string Cedula { get; set; }
    
    [Required(ErrorMessage = "Por favor incluya una dirección de correo electrónico")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    [MaxLength(50)]
    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",
    ErrorMessage = "Ingrese una dirección de correo válida")]
    public string Email { get; set; }
    public bool? Foto { get; set; }
    public virtual ICollection<Cita> Cita { get; set; }
    

  

    }
}

