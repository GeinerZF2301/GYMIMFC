using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
