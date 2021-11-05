using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GYMIMFC.Models
{
    public class Matricula
    {
        [Key]
        public virtual DateTime Horario { get; set; }

        public virtual short idCliente { get; set; }

        public virtual short idEmpleado { get; set; }

        public virtual short idServicio { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Empleado Empleado { get; set; }

        public virtual Servicios Servicios { get; set; }


    }
}
