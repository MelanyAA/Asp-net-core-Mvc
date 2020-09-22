using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace BlogCore.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoriasController : Controller
    {
        //Instancia al contenedor de Trabajo
        private readonly IContenedorTrabajo _contenedorTrabajo;

            public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        //Index metodo
        [HttpGet] // httpGet por que solo nos muestra
        public IActionResult Index()
        {
            return View();
        }

        //Metodo Create
        [HttpGet] // httpGet por que solo nos muestra el Formulario
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Para evitar haqueo de Formulario
        public IActionResult Create(Categoria categoria)
        { 
        if(ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria); // en el contenedor de trabajo acedemos a la Categoria y agregamos todo lo q imgresamos 
                _contenedorTrabajo.Save();   // guaradamos
                return RedirectToAction(nameof(Index));// Nos redireccionara al Index
            }

            return View(categoria);
        }


        //Boton editar
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria(); // intanceamos la entidad categoria 
            categoria = _contenedorTrabajo.Categoria.Get(id); // buscamos en el ciontenedor de trabajo la categoria con el metodo get optenemos el adi y lo pasamos a la variable categoria
            if(categoria == null)
            {
                return NotFound(); // Retorna un error
            }
            return View(categoria); // si la categoria es Valida nos retorna su vista
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Para evitar haqueo de Formulario
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria); // en el contenedor de trabajo acedemos a la Categoria y agregamos todo lo q imgresamos 
                _contenedorTrabajo.Save();   // guaradamos
                return RedirectToAction(nameof(Index));// Nos redireccionara al Index
            }

            return View(categoria);
        }


        #region Llamadas a las API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }


        //EL metodo eliminar va dentro de Llamado al API
        [HttpDelete]

        public IActionResult Delete(int id)
        {

            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
             if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error Borrando Categoria" });
            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoria Borrada Correctamente" });

        }
        #endregion
    }
}
