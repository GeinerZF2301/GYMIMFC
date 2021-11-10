using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Models
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }
        public int? TipoUsuarioId { get; set; }
        public string Nombre { get; set; }
        public int? Bhabilitado { get; set; }
        //public virtual ICollection<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
