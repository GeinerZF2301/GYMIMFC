using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klinica.Models
{
    public class SD
    {
        //tipoUsuarioId = 1=Admin 2=Registrador 3=Citas 4=Consultas
        static public Boolean Admin = false; 
        static public Boolean Registrador = false; 
        static public Boolean Citas = false;
        static public Boolean Consultas = false;
        static public string NombreUsuario = "";
    }
}
