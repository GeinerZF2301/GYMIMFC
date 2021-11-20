using GYMIMFC.Migrations;
using GYMIMFC.Models;
using GYMIMFC.Filter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace GYMIMFC.Controllers
{
    public class TipoUsuariosController : Controller
    {
        public static List<TipoUsuario> lista;

        private readonly ApplicationDbContext _db;
        public TipoUsuariosController(ApplicationDbContext db)
        {
            _db = db;
        }
        private void DeterminarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<TipoUsuario>().OrderByDescending(
                e => e.TipoUsuarioId).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.TipoUsuarioId + 1;
            }
            else
            {
                ViewBag.ID = 1;
            }
        }
        public IActionResult Index(TipoUsuario oTipoUsuario)
        {
            DeterminarUltimoRegistro();
            List<TipoUsuario> listaTipoUsu = new List<TipoUsuario>();
            listaTipoUsu = (from tipousu in _db.TipoUsuario
                            where tipousu.Bhabilitado == 1
                            select new TipoUsuario
                            {
                                TipoUsuarioId = tipousu.TipoUsuarioId,
                                Nombre = tipousu.Nombre,
                            }).ToList();
            if (oTipoUsuario.Nombre != null && oTipoUsuario.TipoUsuarioId != 0)
            {

                ViewBag.Nombre = oTipoUsuario.Nombre;
                ViewBag.TipoUsuarioId = oTipoUsuario.TipoUsuarioId;
            }

            lista = listaTipoUsu;
            return View(listaTipoUsu);
        }
        [HttpPost]
        public IActionResult Delete(int? TipoUsuarioId)
        {
            var Error = "";
            try
            {
                TipoUsuario _TipoUsuario = _db.TipoUsuario
                   .Where(t => t.TipoUsuarioId == TipoUsuarioId).First();
                _db.TipoUsuario.Remove(_TipoUsuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public string Edited(TipoUsuario _TipoUsuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid && _TipoUsuario == null)
                {
                    //Escribimos nuestra logica
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();

                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-danger'>";
                        rpta += item;
                        rpta += "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    rpta = "OK";
                    _db.TipoUsuario.Update(_TipoUsuario);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public JsonResult Details(int? id)
        {
            TipoUsuario _Tipousuario = (from t in _db.TipoUsuario
                                        where t.TipoUsuarioId == id
                                        select t).DefaultIfEmpty().Single();
            return Json(_Tipousuario);
        }
        public JsonResult Edit(int? id)
        {
            TipoUsuario _Tipousuario = (from t in _db.TipoUsuario
                                        where t.TipoUsuarioId == id
                                        select t).DefaultIfEmpty().Single();
            return Json(_Tipousuario);
        }

        public string Create(TipoUsuario _TipoUsuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid && _TipoUsuario == null)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-danger'>";
                        rpta += item;
                        rpta += "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    rpta = "OK";
                    TipoUsuario usertype = new TipoUsuario
                    {
                        Nombre = _TipoUsuario.Nombre,
                        Bhabilitado = _TipoUsuario.Bhabilitado
                    };
                    _db.TipoUsuario.Add(usertype);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
    }
}
