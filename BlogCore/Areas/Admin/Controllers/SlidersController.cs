using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        //Metodo Post de Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid) //Si el Modelo Es valido
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // Para Acceder a la ruta principal que seria al wwwroot carpeta
                var archivos = HttpContext.Request.Form.Files;

                
                
                    //Nuevo Articulo
                    string nombrearchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders"); //Ruta donde se van aguardar los archivos wwwroot carpeta imagen carpeta articulos
                    var extension = Path.GetExtension(archivos[0].FileName); //extencion del Archivo

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombrearchivo + extension), FileMode.Create)) //concatenamos la extencion **subida** 
                    {

                        archivos[0].CopyTo(fileStreams);

                    }

                    slider.UrlImagen = @"\imagenes\sliders\" + nombrearchivo + extension;
                 

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }

            return View();

        }

        //Editar Get
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }


        //Metodo Post de Editar

        [HttpPost]
        
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid) //Si el Modelo Es valido
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // Para Acceder a la ruta principal que seria al wwwroot carpeta
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDb = _contenedorTrabajo.Slider.Get(slider.Id); // Variable para acededer

                if (archivos.Count() > 0)

                {
                    //Editar Imagenes
                    string nombrearchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders"); //Ruta donde se van aguardar los archivos wwwroot carpeta imagen carpeta articulos
                    var extension = Path.GetExtension(archivos[0].FileName); //extencion del Archivo
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Suvimos nuevamente el Archivo

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombrearchivo + nuevaExtencion), FileMode.Create)) //concatenamos la nuevaextencion **subida** 
                    {

                        archivos[0].CopyTo(fileStreams);

                    }

                    slider.UrlImagen = @"\imagenes\sliders\" + nombrearchivo + nuevaExtencion;// le pasamos la nueva extencion
                 

                    _contenedorTrabajo.Slider.Update(slider);// en el SliderRepositorio creamos el metodo de Update para actualizar
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquies Cuando la Imagen ya existe y no remplaza
                    //Debe Conservar la que ya esta en la Base de Datos

                    slider.UrlImagen = sliderDesdeDb.UrlImagen;
                }

                _contenedorTrabajo.Slider.Update(slider);// en el ArticuloRepositorio creamos el metodo de UpDate para actualizar
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


      
       


        #region Llamadas a las API
        [HttpGet]
        public IActionResult GetAll()
        {
            // return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria")}); //Inclue los parametros de categoria

            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });

        }


        // Metodo eliminar
        [HttpDelete]

        public IActionResult Delete(int id)
        {

            var objFromDb = _contenedorTrabajo.Slider.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error Borrando Slider" });
            }
            _contenedorTrabajo.Slider.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider Borrado Correctamente" });

        }


        #endregion

    }
}
