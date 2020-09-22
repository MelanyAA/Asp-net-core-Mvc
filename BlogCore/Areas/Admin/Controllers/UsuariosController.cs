using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogCore.Acceso.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            
            var claimsIdentity =(ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);// NameIdentifier Este Propiedad se usa para el Id Unico de el Usuario
            return View(_contenedorTrabajo.Usuario.GetAll(u =>u.Id !=usuarioActual.Value));//Accedemos la lista con los usuarios menos el que esta logiado

        }
    }
}
