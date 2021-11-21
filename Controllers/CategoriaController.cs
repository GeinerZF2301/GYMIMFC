using GYMIMFC.Migrations;
using GYMIMFC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly ApplicationDbContext _db;
        static List<Categoria> listaCategorias = new List<Categoria>();
        static List<Categoria> lista = new List<Categoria>();
        public CategoriaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaCategorias = (from Categoria in _db.Categoria
                                 select new Categoria
                                 {
                                     idCategoria = Categoria.idCategoria,
                                     NombreCategoria = Categoria.NombreCategoria,
                                     Descripcion = Categoria.Descripcion.Substring(0, 85) + "..."
                                 }).ToList();
            lista = listaCategorias;
            return View(listaCategorias);
        }
        private void DeterminarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Categoria>().OrderByDescending(
                e => e.idCategoria).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.idCategoria + 1;
            }
            else
                ViewBag.ID = 1;

        }
        public IActionResult Create()
        {
            DeterminarUltimoRegistro();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            int nVeces = 0;
            string Error = "";
            try
            {
                nVeces = _db.Categoria.Where(e =>
                                       e.idCategoria == categoria.idCategoria).Count();

                if (!ModelState.IsValid || nVeces >= 1)
                {
                    if (nVeces > 1) ViewBag.msgError = "El nombre de la Categoria" +
                              " ya está registrado";
                    DeterminarUltimoRegistro();
                    return View(categoria);
                }
                else
                {
                    Categoria _categoria = new Categoria();
                    _categoria.NombreCategoria = categoria.NombreCategoria;
                    _categoria.Descripcion = categoria.Descripcion;
                    _db.Categoria.Add(_categoria);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
