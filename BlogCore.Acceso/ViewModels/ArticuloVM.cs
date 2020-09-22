using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.ViewModels
{
   public class ArticuloVM
    {

        public Articulo Articulo { get; set; }

        public IEnumerable<SelectListItem> ListaCategoria { get; set; } //Creamos las Lista de Categoria
    }
}
