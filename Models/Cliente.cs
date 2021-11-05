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
        public virtual short idCliente { get; set; }

        public virtual string Nombre { get; set; }

        public virtual DateTime FechaNacimiento { get; set; }

        public virtual double Peso { get; set; }

        public virtual double IMC { get; set; }

        public virtual double Altura { get; set; }

        public virtual short Edad { get; set; }

        public virtual DateTime FechaPagoMatricula { get; set; }

        public virtual string PlanEntrenamiento { get; set; }

        public virtual string Cedula { get; set; }

        public virtual IList<Matricula> Matriculas { get; set; }

    }
}
