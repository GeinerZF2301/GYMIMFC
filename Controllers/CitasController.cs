/*using GYMIMFC.Migrations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GYMIMFC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYMIMFC.Controllers
{ 
 public class CitasController : Controller
{
    private readonly ApplicationDbContext _db;
    List<Cita> listaCitas = new List<Cita>();

    static private string _Fecha;
    public CitasController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        listaCitas = (from citas in _db.Citas

                      join clientes in _db.Cliente
                      on citas.clienteId equals
                      clientes.idCliente

                      join empleado in _db.Empleado
                      on citas.idEmpleado equals
                      empleado.idEmpleado
                      join categoria in _db.Categoria
                      on citas.idCategoria equals
                      categoria.idCategoria

                      select new Cita
                      {
                          citaId = citas.citaId,
                          nombreCliente = clientes.nombreCliente +
                                       " " + clientes.Apellidos,
                          idEmpleado = empleado.idEmpleado,
                          nombreEmpleado = empleado.NombreEmpleado + " " + empleado.Apellidos,
                          nombreServicio = categoria.NombreCategoria,
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
        List<SelectListItem> listaEmpleados = new List<SelectListItem>();
            listaEmpleados = (from empleado in _db.Empleado
                              orderby empleado.NombreEmpleado
                              join categoria in _db.Categoria
                              on empleado.CategoriaId equals categoria.idCategoria
                              select new SelectListItem
                              {
                                  Text = empleado.Apellidos + ", " + empleado.NombreEmpleado
                                              + " - " + categoria.NombreCategoria,

                                  Value = empleado.
                              }
                                   ).ToList();
        ViewBag.ListaEmpleados = listaEmpleados;
    }
    [HttpGet]
    public IActionResult Create(string PacienteId)
    {
        cargarEmpleados();
        DeterminarUltimoRegistro();
            if (PacienteId == null)
            {
            }
            else
            {
                BuscarPaciente(PacienteId);
            }
            ViewBag.Controlador = "Citas";
        ViewBag.Accion = "Create";
        return View();
    }
    private void BuscarPaciente(string ClienteId)
    {
        Cliente oCliente = _db.Cliente
       .Where(p => p.idCliente == ClienteId).FirstOrDefault();
        if (oCliente != null)
        {
            ViewBag.ClienteID = oCliente.idCliente;
            ViewBag.Nombre = oCliente.nombreCliente + " " + oCliente.Apellidos;
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
                                 idCliente = clientes.idCliente,
                                 nombreCliente = clientes.nombreCliente,
                                 Apellidos = clientes.Apellidos,
                                 IMC = clientes.IMC,
                                 Altura = clientes.Altura,
                                 Peso = clientes.Peso,
                                 PlanEntrenamiento = clientes.PlanEntrenamiento,
                             }).ToList();
        return View(listaClientes);
    }

    private int buscarCategoria(int empleadoId)
    {
        int categoriaID = 0;
        Empleado oEmpleado = _db.Empleado
            .Where(m => m.idEmpleado == empleadoId).SingleOrDefault();
        if (oEmpleado != null)
        {
            categoriaID = oEmpleado.CategoriaId;
        }
        return categoriaID;
    }
    private void buscarCita(int Id)
    {
        listaCitas = (from _citas in _db.Citas
                      join _empleados in _db.Empleado on _citas.idEmpleado equals _empleados.idEmpleado
                      join _categorias in _db.Categoria on _citas.idCategoria
                                                          equals _categorias.idCategoria
                      join _clientes in _db.Cliente on _citas.clienteId equals _clientes.idCliente
                      where _citas.CitaId == Id
                      select new Cita
                      {
                          citaId = _citas.CitaId,
                          FechaCita = _citas.FechaCita,
                          clienteId = _citas.PacienteId,
                          idEmpleado = _citas.empleadoId,
                          nombreCliente = _clientes.nombreCliente + " " + _clientes.Apellidos,
                          nombreEmpleado = _empleados.nombreEmpleado + " " + _empleados.Apellidos,
                          nombreServicio = _categorias.nombreCategorias,
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
    public IActionResult Deleted(int Id)
    {
        string Error = "";
        try
        {
            Cita oCita = _db.Citas
                .Where(c => c.citaId == Id).First();
            if (oCita != null)
            {
                _db.Citas.Remove(oCita);
                _db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Created(Cita cita)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                cargarEmpleados();
            }
            else
            {
                var citaId = buscarCategoria(cita.citaId);
                Cita _cita = new Cita();
                //_cita.CitaId = cita.CitaId;
                _cita.clienteId = cita.clienteId;
                _cita.idEmpleado = cita.idEmpleado;
                _cita.FechaCita = cita.FechaCita;
                _cita.idCategoria = cita.idCategoria;
                _db.Citas.Add(_cita);
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
        return View("Create");

    }
    [HttpGet]
    public IActionResult Edit(int Id)
    {
        buscarCita(Id);
        Cita _cita = new Cita();
        foreach (Cita item in listaCitas)
        {
            _cita.citaId = item.citaId;
            _cita.FechaCita = item.FechaCita.Date;
            _cita.clienteId = item.clienteId;
            _cita.nombreCliente = item.nombreCliente;
            _cita.idEmpleado = item.idEmpleado;
            _cita.nombreEmpleado = item.nombreCliente;
            _cita.idCategoria = item.idCategoria;
            _cita.nombreServicio = item.nombreServicio;
         
        }
        ViewBag.Medico = _cita.idEmpleado;
        ViewBag.FechaCita = _cita.FechaCita.ToString("yyyy-MM-dd");
        _Fecha = ViewBag.FechaCita;
        cargarEmpleados();
        return View(_cita);
    }
    [HttpPost]
    public IActionResult Edit(Cita cita)
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
                _cita.citaId = cita.citaId;
                _cita.clienteId = cita.clienteId;
                _cita.idEmpleado = cita.idEmpleado;
                _cita.FechaCita = cita.FechaCita;
                _cita.idCategoria = buscarCategoria(cita.idEmpleado);
                _db.Citas.Update(_cita);
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
*/ 


