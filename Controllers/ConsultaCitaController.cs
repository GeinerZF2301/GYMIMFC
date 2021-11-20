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
    public class ConsultaCitaController : Controller
    {
        private readonly ApplicationDbContext _db;
        List<CitaGym> listaCitas = new List<CitaGym>();
        static List<CitaGym> lista = new List<CitaGym>();

        public ConsultaCitaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(DateTime FechaInicio, DateTime FechaFinal)
        {

            String _FechaInicio = FechaInicio.ToString("dd-MM-yyyy");
            String _FechaFinal = FechaFinal.ToString("dd-MM-yyyy");
            //String strFecha = DateTime.Now.ToString();
            //"23/05/2021 0:00:00"
            //"2021-05-23"

            if ((_FechaInicio == "01-01-0001") || (_FechaFinal == "01-01-0001"))
            {
                listaCitas = (from citas in _db.Cita
                              join Clientes in _db.Cliente
                              on citas.clienteId equals
                              Clientes.ClienteId

                              join Empleado in _db.Empleado
                              on citas.EmpleadoId equals
                              Empleado.EmpleadoId

                              join Servicios in _db.Servicios
                              on citas.ServicioId equals
                              Servicios.ServicioId
                              orderby (citas.FechaCita)

                              select new CitaGym
                              {
                                  CitaId = citas.citaId,
                                  NombreCliente = Clientes.nombreCliente +
                                              " " + Clientes.Apellidos,
                                  EmpleadoId = Empleado.EmpleadoId,
                                  NombreEmpleado = Empleado.Nombre + " " + Empleado.Apellidos,
                                  NombreServicio = Servicios.Nombre,
                                  FechaCita = citas.FechaCita
                              }).ToList();
                //{7/8/2021 00:00:00}
                var ocita =
                _db.Set<Cita>()
                .OrderByDescending(t => t.FechaCita)
                .LastOrDefault();

                var _Cita =
                          _db.Set<Cita>()
                          .OrderByDescending(t => t.FechaCita)
                          .FirstOrDefault();

                String strDia, strMes, strFecha;
                strDia = ocita.FechaCita.Day.ToString();
                strMes = ocita.FechaCita.Month.ToString();

                if (strDia.Length == 1) strDia = "0" + strDia;
                if (strMes.Length == 1) strMes = "0" + strMes;
                strFecha = ocita.FechaCita.Year.ToString() + "-" +
                                         strMes + "-" + strDia;
                ViewBag.fechaInicio = strFecha;
                //2021-08-07
                strDia = _Cita.FechaCita.Day.ToString();
                strMes = _Cita.FechaCita.Month.ToString();

                if (strDia.Length == 1) strDia = "0" + strDia;
                if (strMes.Length == 1) strMes = "0" + strMes;
                strFecha = _Cita.FechaCita.Year.ToString() + "-" +
                                         strMes + "-" + strDia;
                ViewBag.fechaFinal = strFecha;
            }
            else
            {
                listaCitas = (from citas in _db.Cita

                              join Clientes in _db.Cliente
                              on citas.clienteId equals
                              Clientes.ClienteId

                              join Empleado in _db.Empleado
                              on citas.EmpleadoId equals
                              Empleado.EmpleadoId

                              join Servicios in _db.Servicios
                              on citas.ServicioId equals
                              Servicios.ServicioId

                              where (citas.FechaCita >= FechaInicio
                                  && citas.FechaCita <= FechaFinal)

                              select new CitaGym
                              {
                                  CitaId = citas.citaId,
                                  NombreCliente = Clientes.nombreCliente +
                                              " " + Clientes.Apellidos,
                                  EmpleadoId = Empleado.EmpleadoId,
                                  NombreEmpleado = Empleado.Nombre + " " + Empleado.Apellidos,
                                  NombreServicio = Servicios.Nombre,
                                  FechaCita = citas.FechaCita
                              }).ToList();
                ViewBag.fechaInicio = FechaInicio.ToString("yyyy-MM-dd");
                ViewBag.fechaFinal = FechaFinal.ToString("yyyy-MM-dd");

            }
            lista = listaCitas;
            return View(listaCitas);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Cita ID", "Cliente", "Empleado", "Servicios", "Fecha Cita" };
            string[] nombrePropiedades = { "CitaId", "NombreCliente", "NombreEmpleado",
                                           "NombreServicio","FechaCita"};
            byte[] buffer = util.GenerarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Cita ID", "Cliente", "Empleado", "Servicios", "Fecha Cita" };

            string[] nombrePropiedades = { "CitaId", "NombreCliente", "NombreEmpleado",
                                           "NombreServicio","FechaCita"};
            string titulo = "Reporte de Citas Cliente";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
