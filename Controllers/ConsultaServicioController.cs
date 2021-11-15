using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GYMIMFC.Migrations;
using GYMIMFC.Models;
using GYMIMFC.Models.ViewModels;

namespace GYMIMFC.Controllers
{
    public class ConsultaServicioController : Controller
    {
        private readonly ApplicationDbContext _db;
        static List<Servicios> listaServicios = new List<Servicios>();
        public ConsultaServicioController(ApplicationDbContext db)
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
    }
}
