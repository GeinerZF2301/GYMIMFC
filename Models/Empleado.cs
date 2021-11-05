using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public class Empleado
    {
        [Key]
        public virtual short idEmpleado { get; set; }

        public virtual string Puesto { get; set; }

        public virtual string ZonaPuesto { get; set; }

        public virtual DateTime FechaIngreso { get; set; }

        public virtual DateTime TiempoContrato { get; set; }

        public virtual string Cedula { get; set; }

        public virtual IList<Matricula> Matriculas { get; set; }
    }
}
