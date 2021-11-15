using GYMIMFC.Migrations;
using GYMIMFC.Models;
using GYMIMFC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _db;
        static List<Servicios> listaServicios = new List<Servicios>();
        public ServiciosController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            listaServicios = (from Servicios in _db.Servicios
                              select new Servicios
                              {
                                     ServicioId = Servicios.ServicioId,
                                     Nombre = Servicios.Nombre,
                                     Descripcion =
                                         Servicios.Descripcion.Substring(0, 85) + "..."
                                 }).ToList();

            var model = listaServicios;
            return View("Index", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string dato)
        {
            if (dato == null)
            {
                listaServicios = (from Servicios in _db.Servicios
                                     select new Servicios
                                     {
                                         ServicioId = Servicios.ServicioId,
                                         Nombre = Servicios.Nombre,
                                         Descripcion = Servicios.Descripcion.Substring(0, 85) + "..."
                                     }).ToList();
                return View(listaServicios);
            }
            else
            {
                ViewBag.Dato = dato;
                listaServicios = (from Servicios in _db.Servicios
                                     where (Servicios.Nombre.Contains(dato))
                                     select new Servicios
                                     {
                                         ServicioId = Servicios.ServicioId,
                                         Nombre = Servicios.Nombre,
                                         Descripcion = Servicios.Descripcion.Substring(0, 85) + "..."
                                     }).ToList();
                return View(listaServicios);
            }
        }
        private void cargarUltimoRegistro()
        {
            var ultimoRegistro =
                _db.Set<Servicios>().OrderByDescending(e =>
                       e.ServicioId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.ServicioId + 1;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarUltimoRegistro();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Servicios servicios)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(servicios);
                }
                else
                {
                    Servicios _servicios = new Servicios
                    {
                        Nombre = servicios.Nombre,
                        Descripcion = servicios.Descripcion
                    };

                    _db.Servicios.Add(_servicios);
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
            Servicios oServicios = _db.Servicios
                         .Where(e => e.ServicioId == id).First();
            return View(oServicios);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Servicios oServicios = _db.Servicios
                         .Where(e => e.ServicioId == id).First();
            return View(oServicios);
        }
        [HttpPost]
        public IActionResult Edit(Servicios servicios)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(servicios);
                }
                else
                {
                    Servicios _servicios = new Servicios
                    {
                        ServicioId = servicios.ServicioId,
                        Nombre = servicios.Nombre,
                        Descripcion = servicios.Descripcion
                    };
                    _db.Servicios.Update(_servicios);
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
        public IActionResult Delete(int? ServiciosId)
        {
            var Error = "";
            try
            {
                Servicios oServicios = _db.Servicios
                             .Where(e => e.ServicioId == ServiciosId).First();
                _db.Servicios.Remove(oServicios);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        public FileResult exportarPDF(List<Servicios> lista)
        {
            lista = listaServicios;
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Id Servicios", "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "ServicioId", "Nombre", "Descripcion" };
            string titulo = "Reporte de Servicios";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}

