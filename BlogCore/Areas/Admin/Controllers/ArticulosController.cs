using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Acceso.Data.Repository;
using BlogCore.Acceso.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
      
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment; //Propiedad de Subida de Archivos

        public ArticulosController(IContenedorTrabajo contenedorTrabajo,IWebHostEnvironment hostingEnvironment)
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
            //Vio model nos combina la tabal articulo y la lista categori Creamis la carpeta ViewModels en Acceso Datos
            ArticuloVM artivm = new ArticuloVM()
            {

                Articulo = new Models.Articulo(), //Pasamos el Articulo de la Clase ArticuloVM carpera ViewModels
                ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias() // Lista de Categoria
            };
            return View(artivm); // retorno de la Variable


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if(ModelState.IsValid) //Si el Modelo Es valido
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // Para Acceder a la ruta principal que seria al wwwroot carpeta
                var archivos = HttpContext.Request.Form.Files;

                if(artiVM.Articulo.Id ==0)
                {
                    //Nuevo Articulo
                    string nombrearchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos"); //Ruta donde se van aguardar los archivos wwwroot carpeta imagen carpeta articulos
                    var extension = Path.GetExtension(archivos[0].FileName); //extencion del Archivo

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombrearchivo + extension), FileMode.Create)) //concatenamos la extencion **subida** 
                    {

                        archivos[0].CopyTo(fileStreams);

                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombrearchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }

            }

            artiVM.ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias();


            return View(artiVM);
        }

        //Editar 
        [HttpGet]

        public ActionResult Edit(int? id)
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias()

            };

            if (id != null)
            {
                artivm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }
            return View(artivm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ArticuloVM artiVM)
        {
            if (ModelState.IsValid) //Si el Modelo Es valido
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // Para Acceder a la ruta principal que seria al wwwroot carpeta
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id); // Variable para acededer

                if (archivos.Count() > 0)

                {
                    //Editar Imagenes
                    string nombrearchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos"); //Ruta donde se van aguardar los archivos wwwroot carpeta imagen carpeta articulos
                    var extension = Path.GetExtension(archivos[0].FileName); //extencion del Archivo
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

                    if(System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Suvimos nuevamente el Archivo

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombrearchivo + nuevaExtencion), FileMode.Create)) //concatenamos la nuevaextencion **subida** 
                    {

                        archivos[0].CopyTo(fileStreams);

                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombrearchivo + nuevaExtencion;// le pasamos la nueva extencion
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.UpDate(artiVM.Articulo);// en el ArticuloRepositorio creamos el metodo de UpDate para actualizar
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquies Cuando la Imagen ya existe y no remplaza
                    //Debe Conservar la que ya esta en la Base de Datos

                    artiVM.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
                }

                 _contenedorTrabajo.Articulo.UpDate(artiVM.Articulo);// en el ArticuloRepositorio creamos el metodo de UpDate para actualizar
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));


            }

           

            return View();

        }


        //ELiminar

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Exists(rutaImagen);
            }

            if (articuloDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando articulo" });
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Articulo borrado con exito" });


        }


        #region Llamadas a las API
        [HttpGet]
        public IActionResult GetAll()
        {
           // return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria")}); //Inclue los parametros de categoria

            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties :"Categoria")});

        }

       
        #endregion
    }
}
