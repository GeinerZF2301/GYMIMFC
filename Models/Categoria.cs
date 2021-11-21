using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public partial class Categoria
    {
        [Key]
        public virtual int idCategoria { get; set; }
        [Display(Name = "ID Categoria: ")]
        [Required(ErrorMessage = "Debe digitar el ID de la categoria")]
        
        public virtual string NombreCategoria { get; set; }
        [Display(Name = "Nombre Categoria: ")]
        [Required(ErrorMessage = "Debe digitar el nombre de la categoria")]
       
        public virtual string Descripcion { get; set; }
        [Display(Name = "Descripciòn: ")]
        [Required(ErrorMessage = "Debe digitar la descripciòn de la categoria")]
        public virtual IList<Servicios> Servicios { get; set; }

    }
}
