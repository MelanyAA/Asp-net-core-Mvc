using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.Data.Repository
{
    public interface IArticulaRepositorio: IRepository<Articulo>
    {

        void UpDate(Articulo articulo);
       
    }

}
