using BlogCore.Acceso.Data;
using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.Acceso
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        ///FUNCION DE DESBLOQUEAR Y DESBLOQUEAR EL USUARIO
        ///
        public void BloqueaUsuario(string IdUsuario)
        {
            var UsuarioDesdeDB = _db.ApplicationUsers.FirstOrDefault(u => u.Id == IdUsuario);
            UsuarioDesdeDB.LockoutEnd = DateTime.Now.AddDays(100); //Las veces que intento de fallo las que ustedes quieran
            _db.SaveChanges();

        }

        public void DesbloquearUsuario(string IdUsuario)
        {

            var UsuarioDesdeDB = _db.ApplicationUsers.FirstOrDefault(u => u.Id == IdUsuario);
            UsuarioDesdeDB.LockoutEnd = DateTime.Now; //Aqui se esta desbloqueando el Ususario
            _db.SaveChanges();

        }
    }
}
