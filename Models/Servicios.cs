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

        public virtual short idServicio { get; set; }

        public virtual string NombreServicio { get; set; }

        public virtual string NumeroMatricula { get; set; }

        public virtual short idCategoria { get; set; }

        public virtual IList<Matricula> Matriculas { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria Categoria { get; set; }

    }
}
