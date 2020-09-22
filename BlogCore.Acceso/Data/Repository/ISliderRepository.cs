using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Acceso.Data.Repository
{
    public interface ISliderRepository : IRepository<Slider>
    {

        void Update(Slider slider);
    }
}
