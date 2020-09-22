using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es Obligatorio")]
        [Display(Name="Nombre delArticulo")]
        public string Nombre { get; set; }


        [Required(ErrorMessage ="La descripcion es Requerida")]
        public string Descripcion { get; set; }

        [Display(Name="Fecha de Creación")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string UrlImagen { get; set; }

        //Es un punto de relacion con Categoria
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } // matener la llama de categoria en mayuscula por json 
    }
}
