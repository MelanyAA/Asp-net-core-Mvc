using BlogCore.Acceso.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlogCore.Acceso.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {

        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {

            var objDesdeBb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeBb.Nombre = slider.Nombre;
            objDesdeBb.Estado = slider.Estado;
            objDesdeBb.UrlImagen = slider.UrlImagen;

            _db.SaveChanges();

        }
    }
}
