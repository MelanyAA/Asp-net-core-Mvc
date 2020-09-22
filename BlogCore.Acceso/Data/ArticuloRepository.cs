using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.Acceso.Data
{
   public class ArticuloRepository : Repository<Articulo>, IArticulaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;

        }

        public void UpDate(Articulo articulo)
        {
            var ObjDesdeBD = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            ObjDesdeBD.Nombre = articulo.Nombre;
            ObjDesdeBD.Descripcion = articulo.Descripcion;
            ObjDesdeBD.UrlImagen = articulo.UrlImagen;
            ObjDesdeBD.CategoriaId = articulo.CategoriaId;

        }
    }
}
