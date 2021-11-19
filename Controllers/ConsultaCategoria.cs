using GYMIMFC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GYMIMFC.Migrations;
namespace Klinica.Controllers
{
    public class ConsultaEnfermedadesController : Controller
    {
        private readonly ApplicationDbContext _db;
        List<Categoria> lista = new List<Categoria>();
        public ConsultaEnfermedadesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Categoria> BuscarCategoria(string nombreCategoria)
        {
            List<Categoria> listacategoria = new List<Categoria>();
            if (nombreCategoria == null || nombreCategoria.Length == 0)
            {
                listacategoria = (from categoria in _db.Categoria
                                     select new Categoria
                                     {
                                         idCategoria = categoria.idCategoria,
                                         NombreCategoria = categoria.NombreCategoria,
                                         Descripcion = categoria.Descripcion
                                     }).ToList();

                ViewBag.Enfermedad = "";
            }
            else
            {
                listacategoria = (from categoria in _db.Categoria
                                     where categoria.NombreCategoria.Contains(nombreCategoria)
                                     select new Categoria
                                     {
                                         idCategoria = categoria.idCategoria,
                                         NombreCategoria = categoria.NombreCategoria,
                                         Descripcion = categoria.Descripcion
                                     }).ToList();

                ViewBag.Categoria = nombreCategoria;
            }
            lista = listacategoria;
            return listacategoria;
        }

        public IActionResult Index()
        {
            lista = BuscarCategoria("");
            return View(lista);
        }
    }
}