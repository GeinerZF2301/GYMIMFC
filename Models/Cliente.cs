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
    [MaxLength(50)]
    public virtual string idCliente { get; set; }
    [Display(Name = "Nombre del Cliente")]
    [StringLength(50)]
    [Required(ErrorMessage = "Debe ingresar el nombre del cliente")]
    public virtual string nombreCliente { get; set; }
    [Display(Name = "Fecha de nacimiento")]
    [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Por favor incluya los apellidos del paciente")]
    public virtual string Apellidos { get; set; }
    public virtual DateTime fechaNacimiento { get; set; }
    [Display(Name = "Registre su peso")]
    [Required(ErrorMessage = "Debe registrar su peso")]

    public virtual double Peso { get; set; }
    [Display(Name = "Registre su IMC")]
    [Required(ErrorMessage = "Debe registrar su IMC")]
    public virtual double IMC { get; set; }
    [Display(Name = "Registre su altura")]
    [Required(ErrorMessage = "Debe registrar su altura")]
    public virtual double Altura { get; set; }
    [Display(Name = "Edad")]
    [Required(ErrorMessage = "Debe registrar su edad")]
    [Range(0,99)]
    public virtual int Edad { get; set; }
    [Display(Name = "Fecha en que pagó la matrícula")]
    [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Registre la fecha en el cual efectuó su matrícula")]
    public virtual DateTime FechaPagoMatricula { get; set; }
    [Display(Name = "Plan de entrenamiento")]
    [Required(ErrorMessage = "Debe describir su plan de entrenamiento")]
    public virtual string PlanEntrenamiento { get; set; }
    [Display(Name = "Cédula del Cliente")]
    [Required(ErrorMessage = "Debe registrar su cédula")]
     public virtual string Cedula { get; set; }

    public virtual IList<Cita> Citas{ get; set; }

    }
}

