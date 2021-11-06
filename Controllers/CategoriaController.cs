using GYMIMFC.Models;
using GYMIMFC.Data;
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
        return View("Index", model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(string dato)
    {
        if (dato == null)
        {
            listaCategoria = (from Categoria in _db.Categoria
                                select new Categoria
                                {
                                    idCategoria = Categoria.idCategoria,
                                    NombreCategoria = Categoria.NombreCategoria,
                                    Descripcion = Categoria.Descripcion.Substring(0, 80) + "..."
                                }).ToList();
            return View(listaCategoria);
        }
        else
        {
            ViewBag.Dato = dato;
            listaCategoria = (from Categoria in _db.Categoria
                                where (Categoria.NombreCategoria.Contains(dato))
                                select new Categoria
                                {
                                    idCategoria = Categoria.idCategoria,
                                    NombreCategoria = Categoria.NombreCategoria,
                                    Descripcion = Categoria.Descripcion.Substring(0, 80) + "..."
                                }).ToList();
            return View(listaCategoria);
        }
    }

    private void cargarUltimoRegistro()
    {
        var ultimoRegistro =
            _db.Set<Categoria>().OrderByDescending(e =>
                    e.idCategoria).FirstOrDefault();
        if (ultimoRegistro == null)
        {
            ViewBag.ID = 1;
        }
        else
        {
            ViewBag.ID = ultimoRegistro.idCategoria + 1;
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        cargarUltimoRegistro();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Categoria categoria)
    {
        string Error = "";
        try
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            else
            {
                Categoria _categoria = new Categoria
                {
                    NombreCategoria = categoria.NombreCategoria,
                    Descripcion = categoria.Descripcion
                };

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

        public IActionResult Details(int id)
        {
            Categoria oCategoria = _db.Categoria
                         .Where(e => e.idCategoria == id).First();
            return View(oCategoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoria);
                }
                else
                {
                    Categoria _categoria = new Categoria
                    {
                        idCategoria = categoria.idCategoria,
                        NombreCategoria = categoria.NombreCategoria,
                        Descripcion = categoria.Descripcion
                    };
                    _db.Categoria.Update(_categoria);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int? idCategoria)
        {
            var Error = "";
            try
            {
                Categoria oCategoria = _db.Categoria
                             .Where(e => e.idCategoria == idCategoria).First();
                _db.Categoria.Remove(oCategoria);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public FileResult exportarPDF(List<Categoria> lista)
        {
            lista = listaCategoria;
           Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Id Especialidad", "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "EspecialidadId", "Nombre", "Descripcion" };
            string titulo = "Reporte de Especialidades";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}



