using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMIMFC.Models
{
    public class Servicios
    {
        [Key]
        [Display(Name = "ID del Servicio")]
        [MaxLength(50)]
        public virtual int idServicio { get; set; }
        [Display(Name = "Nombre del Servicio")]
        [StringLength(50)]
        public virtual string nombreServicio { get; set; }
        [Display(Name = "Numero de matrícula")]
        [MaxLength(50)]
        public virtual string numeroMatricula { get; set; }
        [Display(Name = "ID de categoría")]
        [MaxLength(50)]
        public virtual int idCategoria { get; set; }

        public virtual IList<Cita> Matriculas { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria Categoria { get; set; }

    }
}
   
