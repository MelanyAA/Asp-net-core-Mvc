using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BlogCore.Models;
using BlogCore.Acceso.Data.Repository;
using BlogCore.Acceso.ViewModels;

namespace BlogCore.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()

            {

                Slider = _contenedorTrabajo.Slider.GetAll(),
                ListaArticulo = _contenedorTrabajo.Articulo.GetAll(),
            };

            return View(homeVM);
        }

        public IActionResult Details(int id)
        {

            var ArticuloDesdeDB = _contenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == id);
            return View(ArticuloDesdeDB);


        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
