using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
  public class Categoria
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese un Nombre para cayegoria")]
        [Display(Name ="Nombre Categorìa")]
        public string Nombre{ get; set; }

        [Required]
        [Display(Name = "Orden de Visualizacion")]
        public int Orden { get; set; }

    }
}
