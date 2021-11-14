using GYMIMFC.Models;
using GYMIMFC.Migrations;
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
        static List<Categoria> listaCategoria = new List<Categoria>();
        static List<Categoria> lista = new List<Categoria>();
        public CategoriaController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            listaCategoria = (from Categoria in _db.Categoria
                              select new Categoria
                              {
                                  idCategoria = Categoria.idCategoria,
                                  NombreCategoria = Categoria.NombreCategoria,
                                  Descripcion = Categoria.Descripcion.Substring(0, 80) + "..."
                              }).ToList();

            var model = listaCategoria;
            return View(listaCategoria);
        }

        private void determinarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Categoria>().OrderByDescending(e => e.idCategoria).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.idCategoria = ultimoRegistro.idCategoria + 1;
            }
            else ViewBag.idCategoria = 1;

        }

        public IActionResult Create()
        {
            determinarUltimoRegistro();
            return View();
        }

        public IActionResult Create(Categoria categoria)
        {

            int nVeces = 0;
            string Error = "";
            try
            {
                nVeces = _db.Categoria.Where(e => e.idCategoria == categoria.idCategoria).Count();
                if (!ModelState.IsValid || nVeces >= 1)
                {
                    if (nVeces > 1) ViewBag.msError = "El nombre de la categoria" +
                                     " ya está registrado";
                    determinarUltimoRegistro();
                    return View(categoria);
                }
                else
                {
                    Categoria _categoria = new Categoria();
                    _categoria.NombreCategoria = categoria.NombreCategoria;
                    _categoria.Descripcion = _categoria.Descripcion;
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


