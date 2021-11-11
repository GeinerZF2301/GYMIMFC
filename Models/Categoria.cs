using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public class Categoria
    {
        [Key]

        public virtual int idCategoria { get; set; }
        [Display(Name = "Nombre: ")]
        [Required]
        public virtual string NombreCategoria { get; set; }

        public virtual string Descripcion { get; set; }

        public virtual IList<Servicios> Servicios { get; set; }

    }
}
