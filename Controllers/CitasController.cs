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
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _db;
        List<CitaGym> listaCitas = new List<CitaGym>();

        static private string _Fecha;
        public CitasController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaCitas = (from citas in _db.Cita

                          join clientes in _db.Cliente
                          on citas.clienteId equals
                          clientes.ClienteId

                          join empleado in _db.Empleado
                          on citas.EmpleadoId equals
                          empleado.EmpleadoId

                          join servicios in _db.Servicios
                          on citas.ServicioId equals
                          servicios.ServicioId
                          select new CitaGym
                          {
                              CitaId = citas.citaId,
                              NombreCliente = clientes.nombreCliente +
                                           " " + clientes.Apellidos,
                              EmpleadoId = empleado.EmpleadoId,
                              NombreEmpleado = empleado.Nombre + " " + empleado.Apellidos,
                              NombreServicio = servicios.Nombre,
                              FechaCita = citas.FechaCita
                          }).ToList();
            ViewBag.Controlador = "Citas";
            ViewBag.Accion = "Index";
            return View(listaCitas);
        }
        private void DeterminarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Cita>().OrderByDescending(
                e => e.citaId).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.citaId + 1;
            }
            else
            {
                ViewBag.ID = 1;
            }
        }

        private void cargarEmpleados()
        {
            List<SelectListItem> listaEmpleado = new List<SelectListItem>();
            listaEmpleado = (from empleado in _db.Empleado
                            orderby empleado.Apellidos
                            join servicios in _db.Servicios
                            on empleado.ServicioId equals servicios.ServicioId
                            select new SelectListItem
                            {
                                Text = empleado.Apellidos + ", " + empleado.Nombre
                                            + " - " + servicios.Nombre,
                                Value = empleado.EmpleadoId
                            }
                                   ).ToList();
            ViewBag.listaEmpleado = listaEmpleado;
        }
        private void DeterminarFecha()
        {
            String strFecha = DateTime.Now.ToString();
            //"23/05/2021 0:00:00"
            //"2021-05-23"
            strFecha = strFecha.Substring(6, 4) + "-" + strFecha.Substring(3, 2) +
                       "-" + strFecha.Substring(0, 2);
            ViewBag.Fecha = strFecha;
        }
        public IActionResult Create(string clienteid)
        {
            cargarEmpleados();
            DeterminarUltimoRegistro();
            if (clienteid != null)
            {
                BuscarCliente(clienteid);
            }
            DeterminarFecha();
            ViewBag.Controlador = "Citas";
            ViewBag.Accion = "Create";
            return View();
        }
        private void BuscarCliente(string clienteid)
        {
            Cliente oCliente = _db.Cliente
           .Where(c => c.ClienteId == clienteid).FirstOrDefault();
            if (oCliente != null)
            {
                ViewBag.ClienteID = oCliente.ClienteId;
                ViewBag.Nombre = oCliente.nombreCliente+ " " + oCliente.Apellidos;
                @ViewBag.FechaCita = _Fecha;
            }
            else
            {
                ViewBag.Error = "Cliente no registrado, intente de nuevo!";
            }
        }
        public IActionResult ListarClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            listaClientes = (from clientes in _db.Cliente
                              select new Cliente
                              {
                                  ClienteId = clientes.ClienteId,
                                  nombreCliente = clientes.nombreCliente,
                                  Apellidos = clientes.Apellidos,
                                  Direccion = clientes.Direccion,
                                  Peso =  clientes.Peso,
                                  IMC = clientes.IMC,
                                  Altura = clientes.Altura
                                  
                              }).ToList();
            return View(listaClientes);
        }

        private int buscarServicio(string empleadoId)
        {
            int ServicioId = 0;
            Empleado oEmpleado = _db.Empleado
                .Where(e => e.EmpleadoId == empleadoId).SingleOrDefault();
            if (oEmpleado != null)
            {
                ServicioId = oEmpleado.ServicioId;
            }
            return ServicioId;
        }
        private void buscarCita(int Id)
        {
            listaCitas = (from _citas in _db.Cita
                          join _empleados in _db.Empleado on _citas.EmpleadoId equals _empleados.EmpleadoId
                          join _servicios in _db.Servicios on _citas.ServicioId
                                                              equals _servicios.ServicioId
                          join _clientes in _db.Cliente on _citas.clienteId equals _clientes.ClienteId
                          where _citas.citaId == Id
                          select new CitaGym
                          {
                              CitaId = _citas.citaId,
                              FechaCita = _citas.FechaCita,
                              ClienteId = _citas.clienteId,
                              Comentarios = _citas.Descripcion,
                              EmpleadoId = _citas.EmpleadoId,
                              NombreCliente = _clientes.nombreCliente + " " + _clientes.Apellidos,
                              NombreEmpleado = _empleados.Nombre + " " + _empleados.Apellidos,
                              NombreServicio = _servicios.Nombre,
                          }).ToList();
        }
        public IActionResult Details(int Id)
        {
            buscarCita(Id);
            return View(listaCitas);
        }
        public IActionResult Delete(int Id)
        {
            buscarCita(Id);
            return View(listaCitas);
        }
        [HttpPost, ActionName("Delete")]
        
        public IActionResult Deleted(int id)
        {
            string Error = "";
            try
            {
                Cita oCita = _db.Cita
                    .Where(c => c.citaId == id).First();
                if (oCita != null)
                {
                    _db.Cita.Remove(oCita);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Created(CitaGym cita)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    cargarEmpleados();
                }
                else
                {
                    var citaId = buscarServicio(cita.EmpleadoId);
                    Cita _cita = new Cita();
                    //_cita.CitaId = cita.CitaId;
                    _cita.clienteId = cita.ClienteId;
                    _cita.EmpleadoId = cita.EmpleadoId;
                    _cita.FechaCita = cita.FechaCita;
                    _cita.Descripcion = cita.Comentarios;
                    _cita.ServicioId = citaId;
                    _db.Cita.Add(_cita);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            //Si se quiere caer de nuevo en Create
            //para seguir agregando citas
            cargarEmpleados();
            DeterminarUltimoRegistro();
            DeterminarFecha();
            return View("Create");

        }
        
        public IActionResult Edit(int Id)
        {
            buscarCita(Id);
            CitaGym _citaGym = new CitaGym();
            foreach (CitaGym item in listaCitas)
            {
                _citaGym.CitaId = item.CitaId;
                _citaGym.FechaCita = item.FechaCita.Date;
                _citaGym.ClienteId = item.ClienteId;
                _citaGym.NombreCliente = item.NombreCliente;
                _citaGym.EmpleadoId = item.EmpleadoId;
                _citaGym.NombreEmpleado = item.NombreEmpleado;
                _citaGym.ServicioId = item.ServicioId;
                _citaGym.NombreServicio = item.NombreServicio;
                _citaGym.Comentarios = item.Comentarios;
            }
            ViewBag.Empleado = _citaGym.EmpleadoId;
            ViewBag.FechaCita = _citaGym.FechaCita.ToString("yyyy-MM-dd");
            _Fecha = ViewBag.FechaCita;
            cargarEmpleados();
            return View(_citaGym);
        }
        [HttpPost]
        public IActionResult Edit(CitaGym cita)
        {
            string error;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cita);
                }
                else
                {
                    Cita _cita = new Cita();
                    _cita.citaId = cita.CitaId;
                    _cita.clienteId = cita.ClienteId;
                    _cita.EmpleadoId = cita.EmpleadoId;
                    _cita.FechaCita = cita.FechaCita;
                    _cita.Descripcion = cita.Comentarios;
                    _cita.ServicioId = buscarServicio(cita.EmpleadoId);
                    _db.Cita.Update(_cita);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
