using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.Acceso.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        IEnumerable<SelectListItem> ICategoriaRepositorio.GetListaCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString(),
            });
        }

        void ICategoriaRepositorio.Update(Categoria categoria)
        {
            var ObjDesdeBD = _db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            ObjDesdeBD.Nombre = categoria.Nombre;
            ObjDesdeBD.Orden = categoria.Orden;

            _db.SaveChanges();

        }
    }
}
