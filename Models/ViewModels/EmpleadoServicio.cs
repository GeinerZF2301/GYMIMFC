using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models.ViewModels
{
    public class EmpleadoServicio
    {
        
        [Required(ErrorMessage = "Debe digitar el ID del médico")]
        [Display(Name = "ID:")]
        public string EmpleadoId { get; set; }
        [Required(ErrorMessage = "Debe digitar el nombre del médico")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe digitar los apellidos del médico")]
        [Display(Name = "Apellidos:")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Debe digitar la dirección del médico")]
        [Display(Name = "Dirección:")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe digitar el teléfono del médico")]
        [Display(Name = "Teléfono:")]
        public string TelefonoFijo { get; set; }
        [Required(ErrorMessage = "Debe digitar el # celular del médico")]
        [Display(Name = "# Celular:")]
        public string TelefonoCelular { get; set; }
        public byte Foto { get; set; }
        public int ServicioId { get; set; }
        public string Servicio { get; set; }
        public string msgError { get; set; }
        
    }
}
