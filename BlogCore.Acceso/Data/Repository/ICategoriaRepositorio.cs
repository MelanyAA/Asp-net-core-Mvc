using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.Data.Repository
{
  public interface ICategoriaRepositorio: IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();

        void Update(Categoria categoria);

    }
}
