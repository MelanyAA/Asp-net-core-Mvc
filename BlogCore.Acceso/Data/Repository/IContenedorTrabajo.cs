using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.Data.Repository
{
   public interface IContenedorTrabajo : IDisposable
    {

        ICategoriaRepositorio Categoria { get; }

        IArticulaRepositorio Articulo { get; }

        ISliderRepository Slider { get; }

        IUsuarioRepository Usuario { get; }

        void Save();

    }
}
