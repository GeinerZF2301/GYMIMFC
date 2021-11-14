using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GYMIMFC.Models;
using GYMIMFC.Migrations;
using GYMIMFC.Models;

namespace GYMIMFC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public string _Login(string user, string pass)
        {
            string rpta = "";
            string claveCifrada = Utilitarios.CifrarDatos(pass);
            try
            {
                int nVeces = _db.Usuario.Where(u => u.Nombre == user
                && u.Password == claveCifrada).Count();
                if (nVeces != 0)
                {
                    rpta = "OK";
                    Usuario User = _db.Usuario.Where(u => u.Nombre == user
                            && u.Password == claveCifrada).First();
                    //HttpContext.Session.SetString("usuarioId", User.UsuarioId.ToString());
                    if (User.TipoUsuarioId == 1) SD.Admin = true;
                    if (User.TipoUsuarioId == 2) SD.Registrador = true;
                    if (User.TipoUsuarioId == 3) SD.Citas = true;
                    if (User.TipoUsuarioId == 4) SD.Consultas = true;
                    SD.NombreUsuario = User.Nombre;



                    //tipoUsuarioId = 1=Admin 2=Registrador 3=Citas 4=Consultas
                    //HttpContext.Session.SetString("nombreUsuario", User.Nombre);
                    //HttpContext.Session.SetString("perfilUsuario", User.TipoUsuario.ToString());
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
        public ActionResult CerrarSesion()
        {
            //HttpContext.Session.Remove("usuarioId");
            SD.Admin = false;
            SD.Registrador = false;
            SD.Citas = false;
            SD.Consultas = false;
            return RedirectToAction("Index");
        }


    }
}
