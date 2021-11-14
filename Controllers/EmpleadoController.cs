using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GYMIMFC.Migrations;
using GYMIMFC.Models;
using GYMIMFC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace GYMIMFC.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ApplicationDbContext _db;
        static List<EmpleadoServicio> listaEmpleado =
            new List<EmpleadoServicio>();
        public EmpleadoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaEmpleado = (from Empleado in _db.Empleado
                           join Servicios in _db.Servicios
                           on Empleado.ServicioId equals Servicios.ServicioId
                           select new EmpleadoServicio
                           {
                               EmpleadoId = Empleado.EmpleadoId,
                               Nombre = Empleado.Nombre,
                               Apellidos = Empleado.Apellidos,
                               Direccion = Empleado.Direccion.Length > 50 ?
                                           Empleado.Direccion.Substring(0, 50)
                                           + "..." : Empleado.Direccion,
                               TelefonoFijo = Empleado.TelefonoFijo,
                               TelefonoCelular = Empleado.TelefonoCelular,
                               Servicio = Servicios.Nombre
                           }).ToList();
            return View(listaEmpleado);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string dato)
        {
            if (dato == null)
            {
                listaEmpleado = (from Empleado in _db.Empleado
                               join Servicios in _db.Servicios
                               on Empleado.ServicioId equals Servicios.ServicioId
                               select new EmpleadoServicio
                               {
                                   EmpleadoId = Empleado.EmpleadoId,
                                   Nombre = Empleado.Nombre,
                                   Apellidos = Empleado.Apellidos,
                                   Direccion = Empleado.Direccion.Length > 50 ?
                                           Empleado.Direccion.Substring(0, 50)
                                           + "..." : Empleado.Direccion,
                                   TelefonoFijo = Empleado.TelefonoFijo,
                                   TelefonoCelular = Empleado.TelefonoCelular,
                                   Servicio = Servicios.Nombre
                               }).ToList();
            }
            else
            {
                listaEmpleado = (from Empleado in _db.Empleado
                               join Servicios in _db.Servicios  
                               on Empleado.ServicioId equals Servicios.ServicioId
                               where Servicios.Nombre.Contains(dato)
                               select new EmpleadoServicio
                               {
                                   EmpleadoId = Empleado.EmpleadoId,
                                   Nombre = Empleado.Nombre,
                                   Apellidos = Empleado.Apellidos,
                                   Direccion = Empleado.Direccion.Length > 50 ?
                                           Empleado.Direccion.Substring(0, 50)
                                           + "..." : Empleado.Direccion,
                                   TelefonoFijo = Empleado.TelefonoFijo,
                                   TelefonoCelular = Empleado.TelefonoCelular,
                                   Servicio = Servicios.Nombre
                               }).ToList();
            }
            return View(listaEmpleado);
        }
        private void cargarServicios()
        {
            List<SelectListItem> cargarservicios = new List<SelectListItem>();
            cargarservicios = (from Servicios in _db.Servicios
                                   orderby Servicios.Nombre
                                   select new SelectListItem
                                   {
                                       Text = Servicios.Nombre,
                                       Value = Servicios.ServicioId.ToString()
                                   }
                                   ).ToList();
            ViewBag.ListaServicios = cargarservicios;
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarServicios();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            int nVeces = 0;
            try
            {
                nVeces = _db.Empleado.Where(m => m.EmpleadoId == empleado.EmpleadoId).Count();
                if (!ModelState.IsValid || nVeces >= 1)
                {
                    if (nVeces >= 1) ViewBag.Error = "Este id de Empleado ya existe!";
                    cargarServicios();
                    return View(empleado);
                }
                else
                {
                    Empleado _empleado = new Empleado();
                    _empleado.EmpleadoId = empleado.EmpleadoId;
                    _empleado.Nombre = empleado.Nombre;
                    _empleado.Apellidos = empleado.Apellidos;
                    _empleado.Direccion = empleado.Direccion;
                    _empleado.TelefonoFijo = _empleado.TelefonoFijo;
                    _empleado.TelefonoCelular = _empleado.TelefonoCelular;
                    _empleado.ServicioId = empleado.ServicioId;

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        byte[] p1 = null;
                        using (var fs1 = files[0].OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                        }
                        _empleado.Foto = p1;
                    }
                    _db.Empleado.Add(_empleado);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            cargarServicios();
            Empleado oEmpleado = _db.Empleado
                 .Where(m => m.EmpleadoId == id.ToString()).FirstOrDefault();
            return View(oEmpleado);
        }
        public IActionResult Delete(int id)
        {
            cargarServicios();
            Empleado oEmpleado = _db.Empleado
                 .Where(m => m.EmpleadoId == id.ToString()).FirstOrDefault();
            return View(oEmpleado);
        }
        [HttpPost]
        public IActionResult Delete(int? EmpleadoId)
        {
            string Error = "";
            try
            {
                Empleado oEmpleado = _db.Empleado
                     .Where(m => m.EmpleadoId == EmpleadoId.ToString()).First();
                _db.Empleado.Remove(oEmpleado);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            cargarServicios();
            int recCount = _db.Empleado.Count(e => e.EmpleadoId == id);
            Empleado _empleado = (from p in _db.Empleado
                              where p.EmpleadoId.Trim() == id.Trim()
                              select p).DefaultIfEmpty().Single();
            return View(_empleado);
        }
        [HttpPost]
        public IActionResult Edit(Empleado empleado)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    cargarServicios();
                    return View(empleado);
                }
                else
                {
                    Empleado _empleado = new Empleado();
                    _empleado.EmpleadoId = empleado.EmpleadoId;
                    _empleado.Nombre = empleado.Nombre;
                    _empleado.Apellidos = empleado.Apellidos;
                    _empleado.Direccion = empleado.Direccion;
                    _empleado.TelefonoFijo = _empleado.TelefonoFijo;
                    _empleado.TelefonoCelular = _empleado.TelefonoCelular;
                    _empleado.ServicioId = empleado.ServicioId;
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        byte[] p1 = null;
                        using (var fs1 = files[0].OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                        }

                        _empleado.Foto = p1;
                    }
                    else
                        _empleado.Foto = _empleado.Foto;

                    _db.Empleado.Update(_empleado);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        public FileResult exportar()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "ID Empleado", "Nombre", "Apellidos", "Dirección", "Servicios" };
            string[] nombrePropiedades = { "Empleado ID", "Nombre", "Apellidos", "Direccion", "Servicios" };
            string titulo = "Reporte de Empleados";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, listaEmpleado, titulo);
            return File(buffer, "application/pdf");
        }
    }
 }
