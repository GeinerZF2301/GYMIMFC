using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public class UsuarioTipoUsuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public int TipoUsuarioId { get; set; }
        public string TipoUsuarioNombre { get; set; }
    }
}
