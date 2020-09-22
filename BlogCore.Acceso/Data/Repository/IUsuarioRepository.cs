using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.Data.Repository
{
   public interface IUsuarioRepository :IRepository<ApplicationUser>
    {

        //Metodos de Usuario
        void BloqueaUsuario(string IdUsuario);
        void DesbloquearUsuario(string IdUsuario);
    }
}
