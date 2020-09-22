using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
   public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es Obligatorio")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [Required(ErrorMessage ="La Ciudad es Obligatoria")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Pais es Obligatoria")]
        public string Pais { get; set; }


        //Estos Campos los colocamos en Identity carpeta Acoun Register y en su SubCarpeta Register.cshtml


    }
}
