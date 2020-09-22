using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.ViewModels
{
   public class HomeVM
    {

        public IEnumerable<Slider> Slider { get; set; }

        public IEnumerable<Articulo> ListaArticulo { get; set; }
    }
}
