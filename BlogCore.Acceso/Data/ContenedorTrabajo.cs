using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlogCore.Acceso.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Slider = new SliderRepository(_db);
            Usuario = new UsuarioRepository(_db);
        }
        public ICategoriaRepositorio Categoria { get; private set; }

        public IArticulaRepositorio Articulo { get; private set; }
       
        public ISliderRepository Slider { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
