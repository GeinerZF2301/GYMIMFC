using GYMIMFC.Migrations;
using GYMIMFC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _db;
        static List<Cliente> listaCliente = new List<Cliente>();
        public ClienteController(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Cliente> ListarClientes()
        {

            listaCliente = (from cliente in _db.Cliente
                            select new Cliente
                            {
                                ClienteId = cliente.ClienteId,
                                Nombre = cliente.Nombre,
                                Apellidos = cliente.Apellidos,
                                Direccion = cliente.Direccion,
                                TelefonoContacto = cliente.TelefonoContacto,
                                Email = cliente.Email
                            }).ToList();
            ViewBag.ClienteNombre = "";
            return listaCliente;
        }
        public JsonResult Edit(string id)
        {
            Cliente _cliente = (from p in _db.Cliente
                                where p.ClienteId.Trim() == id.Trim()
                                select p).DefaultIfEmpty().Single();
            return Json(_cliente);
        }
        public JsonResult Details(string id)
        {
            Cliente _cliente = (from p in _db.Cliente
                                where p.ClienteId.Trim() == id.Trim()
                                select p).DefaultIfEmpty().Single();
            return Json(_cliente);
        }
        public string Edited(Cliente _Cliente)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
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
                    ViewBag.Error = rpta;
                }
                else
                {
                    rpta = "OK";
                    _db.Cliente.Update(_Cliente);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public IActionResult Index()
        {
            List<Cliente> ListaCliente = new List<Cliente>();
            ListaCliente = ListarClientes();
            return View(ListaCliente);
        }

        public string Create(Cliente _Cliente)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
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
                    _db.Cliente.Add(_Cliente);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
        public IActionResult Delete(string ClienteId)
        {
            var Error = "";
            try
            {
                Cliente oCliente = _db.Cliente
                   .Where(p => p.ClienteId == ClienteId).First();
                _db.Cliente.Remove(oCliente);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}